from typing import 
def create_model_from_haiku(
    transformed_forward_pass: hk.Transformed,
    sample_batch: BatchExample,
    train_loss: Callable[[BatchExample, BatchTrainOutput], jnp.ndarray],
    eval_metrics: Optional[Mapping[str, metrics.Metric]] = None,
    train_kwargs: Optional[Mapping[str, Any]] = None,
    eval_kwargs: Optional[Mapping[str, Any]] = None) -> Model:
  """Creates Model after applying defaults and haiku specific preprocessing.

  Args:
    transformed_forward_pass: Transformed forward pass from :func:`hk.transform`
    sample_batch: Example input used to determine model parameter shapes.
    train_loss: Loss function for training that outputs per example loss.
    eval_metrics: Mapping of evaluation metric names to
      :class:`~fedjax.metrics.Metric`. These metrics are defined for
      single examples and will be consumed in :func:`evaluate_model`.
    train_kwargs: Keyword arguments passed to model for training.
    eval_kwargs: Keyword arguments passed to model for evaluation.

  Returns:
    Model
  """
  eval_metrics = eval_metrics or {}
  train_kwargs = train_kwargs or {}
  eval_kwargs = eval_kwargs or {}

  @jax.jit
  def init(rng):
    return transformed_forward_pass.init(rng, sample_batch)

  @jax.jit
  def apply_for_train(params, batch, rng=None):
    return transformed_forward_pass.apply(params, rng, batch, **train_kwargs)

  @jax.jit
  def apply_for_eval(params, batch):
    return transformed_forward_pass.apply(params, None, batch, **eval_kwargs)

  return Model(init, apply_for_train, apply_for_eval, train_loss, eval_metrics)



[docs]def create_model_from_stax(
    stax_init: Callable[..., Params],
    stax_apply: Callable[..., jnp.ndarray],
    sample_shape: Tuple[int, ...],
    train_loss: Callable[[BatchExample, BatchTrainOutput], jnp.ndarray],
    eval_metrics: Optional[Mapping[str, metrics.Metric]] = None,
    train_kwargs: Optional[Mapping[str, Any]] = None,
    eval_kwargs: Optional[Mapping[str, Any]] = None,
    input_key: str = 'x') -> Model:
  """Creates Model after applying defaults and stax specific preprocessing.

  Args:
    stax_init: Initialization function returned from :func:`stax.serial`.
    stax_apply: Model forward_pass pass function returned from stax.serial.
    sample_shape: The expected shape of the input to the model.
    train_loss: Loss function for training that outputs per example loss.
    eval_metrics: Mapping of evaluation metric names to
      :class:`~fedjax.metrics.Metric`. These metrics are defined for
      single examples and will be consumed in :func:`evaluate_model`.
    train_kwargs: Keyword arguments passed to model for training.
    eval_kwargs: Keyword arguments passed to model for evaluation.
    input_key: Key name for the input in batch mapping.

  Returns:
    Model
  """
  eval_metrics = eval_metrics or {}
  train_kwargs = train_kwargs or {}
  eval_kwargs = eval_kwargs or {}

  @jax.jit
  def init(rng):
    _, params = stax_init(rng, sample_shape)
    return params

  @jax.jit
  def apply_for_train(params, batch, rng=None):
    return stax_apply(params, batch[input_key], rng=rng, **train_kwargs)

  @jax.jit
  def apply_for_eval(params, batch):
    return stax_apply(params, batch[input_key], **eval_kwargs)

  return Model(init, apply_for_train, apply_for_eval, train_loss, eval_metrics)



@functools.partial(jax.jit, static_argnums=0)
def _evaluate_model_step(model: Model, params: Params, batch: BatchExample,
                         stat: metrics.Stat) -> Dict[str, metrics.Stat]:
  """Evaluates model for one batch and returns merged Stat.

  Args:
    model: Model container with apply_for_eval and eval_metrics.
    params: Pytree of model parameters to be evaluated.
    batch: Batch of N examples.
    stat: Intermediate Stat from the previous step to be accumulated in the
      current step.

  Returns:
    A dictionary of intermediate evaluation Stats.
  """
  try:
    mask = batch[client_datasets.EXAMPLE_MASK_KEY].astype(jnp.bool_)
  except KeyError:
    mask = jnp.ones([len(next(iter(batch.values())))], dtype=jnp.bool_)
  pred = model.apply_for_eval(params, batch)
  new_stat = {
      k: metrics.evaluate_batch(metric, batch, pred, mask)
      for k, metric in model.eval_metrics.items()
  }
  return jax.tree_util.tree_map(
      lambda a, b: a.merge(b),
      stat,
      new_stat,
      is_leaf=lambda v: isinstance(v, metrics.Stat))


[docs]def evaluate_model(model: Model, params: Params,
                   batches: Iterable[BatchExample]) -> Dict[str, jnp.ndarray]:
  """Evaluates model for multiple batches and returns final results.

  This is the recommended way to compute evaluation metrics for a given model.

  Args:
    model: Model container.
    params: Pytree of model parameters to be evaluated.
    batches: Multiple batches to compute and aggregate evaluation metrics over.
      Each batch can optional contain a feature keyed by
      client_datasets.MASK_KEY (see :meth:`ClientDataset.padded_batch` ).

  Returns:
    A dictionary of evaluation :class:`~fedjax.metrics.Metric` results.
  """
  stat = {k: metric.zero() for k, metric in model.eval_metrics.items()}
  for batch in batches:
    stat = _evaluate_model_step(model, params, batch, stat)
  return jax.tree_util.tree_map(
      lambda x: x.result(), stat, is_leaf=lambda v: isinstance(v, metrics.Stat))