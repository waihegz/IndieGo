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

namespace IndieGo.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        //used to access Global variables 
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public MainPage()
        {
            InitializeComponent();


        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
            objMainWindows.myFrame.Content = objMainWindows.mainMap;
        }
    }
}
