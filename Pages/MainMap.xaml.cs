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
    /// Interaction logic for MainMap.xaml
    /// The code to simulate continous movement was adapted from
    /// </summary>
    /// 

    public partial class MainMap : Page
    {
        int speed = 20;
        Random random;
        public MainMap()
        {
            InitializeComponent();
            myCanvas.Focus();
            random = new Random();
        }

        private void gym_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
            objMainWindows.myFrame.Content = new GymBattle();
        }

        private void inventory_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
            objMainWindows.myFrame.Content = objMainWindows.inventory;
        }

        private void myCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && (Canvas.GetTop(trainer) + (trainer.Height * 2) < Application.Current.MainWindow.Height))
            {
                Canvas.SetTop(trainer, Canvas.GetTop(trainer) + speed);
            }
            if (e.Key == Key.Up && Canvas.GetTop(trainer) > 0)
            {
                Canvas.SetTop(trainer, Canvas.GetTop(trainer) - speed);
            }
            if (e.Key == Key.Left && Canvas.GetLeft(trainer) > 0)
            {
                Canvas.SetLeft(trainer, Canvas.GetLeft(trainer) - speed);
            }
            if (e.Key == Key.Right && (Canvas.GetLeft(trainer) + (trainer.Width * 2) < Application.Current.MainWindow.Width))
            {
                Canvas.SetLeft(trainer, Canvas.GetLeft(trainer) + speed);
            }
            CheckIntersection();
        }

        private void relocate(int leftPos = 543, int topPos = 333)
        {
            Canvas.SetLeft(trainer, leftPos);
            Canvas.SetTop(trainer, topPos);
        }
        
        private void CheckIntersection()
        {
            //abstraction of the player position
            Rect player = new Rect(Canvas.GetLeft(trainer), Canvas.GetTop(trainer), trainer.Width, trainer.Height);


            foreach (var container in myCanvas.Children.OfType<Rectangle>())
            {
                //abstraction of the contianer position 
                Rect hitBox = new Rect(Canvas.GetLeft(container), Canvas.GetTop(container), container.Width, container.Height);
                if (container.Tag != null)
                {
                    string containerID = (string)container.Tag;
                    if(containerID == "Gym")
                    {
                        //check if the player intersect a gym

                       if(player.IntersectsWith(hitBox))
                        {
                            //relocate to the center and go to gym page
                            relocate();
                            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
                            objMainWindows.myFrame.Content = new GymBattle();

                        }
                    }
                    else if(containerID ==  "egg")
                    {
                        if (player.IntersectsWith(hitBox))
                        {
                            //relocate player to center
                            relocate();
                            //relocate the egg
                            int direction = random.Next(0, 2);
                            int leftPos = 0;
                            int topPos = 0;
                            topPos = random.Next(100, 600);
                            if (direction == 0)
                            {
                                //new egg on left hand side 
                                 leftPos = random.Next(0, 160)+100;
                            }
                            else
                            {
                                //new egg on left hand side 
                                leftPos = random.Next(0, 160) + 650;
                            }
                            Canvas.SetLeft(egg, leftPos);
                            Canvas.SetTop(egg, topPos);
                            //go to miniGame
                            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
                            objMainWindows.myFrame.Content = new MiniGame();
                        }

                    }
                }
            }

        }
        private void minGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
            objMainWindows.myFrame.Content = new MiniGame();
        }
    }
}
