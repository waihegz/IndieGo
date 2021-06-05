using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        Models.Player myPlayer;
        Presenters.InventoryModel myModel;

        public Inventory()
        {

            InitializeComponent();
            //start the InventoryModel singleton
            myPlayer = Models.Player.Instance;
            myModel = Presenters.InventoryModel.Instance(combo, myPlayer);
            combo.ItemsSource = myPlayer.MyCharachers;
            combo.SelectedIndex = 0;
            this.DataContext = myModel;
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
            objMainWindows.myFrame.Content = objMainWindows.mainMap;
        }
        private void EvolveClick(object sender, RoutedEventArgs e)
        {
            myModel.Evolve();
        }

        private void PowerUpClick(object sender, RoutedEventArgs e)
        {
            myModel.PowerUp();
        }

        private void SellClick(object sender, RoutedEventArgs e)
        {
            myModel.Sell();
        }

        private void RenameClick(object sender, RoutedEventArgs e)
        {

            try
            {
                string newName = RenameBlock.Text;
                if (newName.Length > 10)
                {
                    FeedBack.Text = "name too long";
                }
                else if (newName.Length == 0)
                {
                    FeedBack.Text = "Enter a name next to the button";
                }
                else
                {
                    myModel.Rename(newName);
                    combo.Items.Refresh();
                }
            }
            catch
            {
                return;
            }
        }
    }
}
