import haiku as hk 
import optax
import fedjax
import numpy as np 
import jax

_SAMPLE_MNIST_BATCH = {
    'x': np.zeros((1, 28, 28, 1), dtype=np.float32),
    'y': np.zeros(1, dtype=np.float32)
    }


_TRAIN_LOSS = lambda b, p: fedjax.metrics.unreduced_cross_entropy_loss(b['y'], p)

_EVAL_METRICS = {
    'loss': fedjax.metrics.CrossEntropyLoss(),
    'accuracy': fedjax.metrics.Accuracy()
}

class mnist_conv(hk.Module):
    def __init__(self, num_classes:int, dropout_rate=0.25):
        super().__init__()
        self._num_classes = num_classes
        self._rate = dropout_rate
    
    def __call__(self, x:jnp.ndarray, is_train: bool):
        x = hk.Conv2D(output_channels=16, kernel_shape=(5, 5), padding='VALID')(x)
        x = (
            hk.MaxPool(
                window_shape=(1, 2, 2, 1), strides=(1, 2, 2, 1),
                padding='VALID')(x))
        x = jax.nn.relu(x)
        x = (
            hk.MaxPool(
                window_shape=(1, 2, 2, 1), strides=(1, 2, 2, 1),
                padding='VALID')(x))
        x = hk.Conv2D(output_channels=32, kernel_shape=(5, 5), padding='VALID')(x)
        x = jax.nn.relu(x)
        x = (
            hk.MaxPool(
                window_shape=(1, 2, 2, 1), strides=(1, 2, 2, 1),
                padding='VALID')(x))
        
        # # if is_train:
        #     x = hk.dropout(rng=hk.next_rng_key(), rate=self._rate, x=x)
        x = hk.Flatten()(x)
        x = hk.Linear(512)(x)
        x = jax.nn.relu(x)
        x = hk.Linear(512)(x)
        x = jax.nn.relu(x)
        # if is_train:
        #     x = hk.dropout(rng=hk.next_rng_key(), rate=self._rate, x=x)
        x = hk.Linear(self._num_classes)(x)
        return x

def create_mnist_cnn(num_classes, dropout_rate=0.25):
        
        def forward_pass(batch, is_train=True):
            return mnist_conv(num_classes, dropout_rate)(batch['x'], is_train)

        transformed_forward_pass = hk.transform(forward_pass)
        return fedjax.create_model_from_haiku(
            transformed_forward_pass=transformed_forward_pass,
            sample_batch=_SAMPLE_MNIST_BATCH,
            train_loss=_TRAIN_LOSS, 
            eval_metrics = _EVAL_METRICS,
            train_kwargs={'is_train': True},
            eval_kwargs={'is_train': False})
        
        
##########Source code for fedjax.create_model
##TODO
##TODO
##TODO

