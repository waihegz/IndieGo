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

namespace IndieGo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Pages.MainPage mainPage;
        public Pages.MainMap mainMap;
        public Pages.Inventory inventory;
        public int trial;
        public Models.Player myPlayer;
        public MainWindow()
        {
            //start and populate player singelton
            myPlayer = Models.Player.Instance;
            //add one pokemon to start the game
            myPlayer.MyCharachers.Add(Models.PokemonGenerator.Get());
            mainPage = new Pages.MainPage();
            inventory = new Pages.Inventory();
            mainMap = new Pages.MainMap();

            InitializeComponent();

            //Navigate to mainPage
            trial = 1;
            myFrame.Content = mainPage;
            
        }
    }
}
