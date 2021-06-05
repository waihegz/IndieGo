using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IndieGo.Pages
{
    /// <summary>
    /// Interaction logic for MiniGame.xaml
    /// </summary>
    public partial class MiniGame : Page
    {
        Presenters.MiniGame myModel;
        DispatcherTimer gameTimer;
        int timeSpent;
        int timeLimit;
        bool gameWon;
        public MiniGame()
        {
            timeSpent = 0;
            timeLimit = 10;
            gameWon = false;
            InitializeComponent();
            myModel = new Presenters.MiniGame();
            DataContext = myModel;
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Tick += TimerEvent;

        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
              MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
              objMainWindows.myFrame.Content = objMainWindows.mainMap;
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            myModel.StartGame();
            yesButton.Visibility = Visibility.Hidden;
            typingInput.Visibility = Visibility.Visible;
            timeWindow.Visibility = Visibility.Visible;
            gameTimer.Start();
            
        }

        private void typingInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (typingInput.Text == myModel.ToType)
            {
                gameWon = true;
                myModel.WinGame();
                typingInput.Visibility = Visibility.Hidden;
                timeWindow.Visibility = Visibility.Hidden;


            }
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            timeSpent++;
            timeWindow.Text = timeSpent.ToString();
            if(timeSpent > timeLimit)
            {
                EndGame();
            }

        }

        private void EndGame()
        {
            timeWindow.Visibility = Visibility.Hidden;
            typingInput.Visibility = Visibility.Hidden;
            gameTimer.Stop();
            myModel.EndGame(gameWon);

        }
    }
}
