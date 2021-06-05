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
    /// Interaction logic for GymBattle.xaml
    /// </summary>
    public partial class GymBattle : Page
    {
        Presenters.GymModel myModel;
        Models.Player myPlayer;
        public GymBattle()
        {
            myModel = new Presenters.GymModel();
            myPlayer = Models.Player.Instance;
            InitializeComponent();
            DataContext = myModel;
            pokemonSelector.ItemsSource = myPlayer.MyCharachers;
            potionSelector.ItemsSource = myPlayer.MyPotions;
        }



        //3 events 
        //attack1 attack2 attack3 
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
            myModel.Heel();
            if(myModel.PotionUsed != null)
            {
                Models.Player.Instance.MyPotions.Remove(myModel.PotionUsed);
            }
            objMainWindows.myFrame.Content = objMainWindows.mainMap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(pokemonSelector.SelectedItem != null)
            {
                myModel.PlayerPokemon = pokemonSelector.SelectedItem as Models.Pokemon;
                pokemonSelector.Visibility = Visibility.Hidden;
                potionSelector.Visibility = Visibility.Hidden;
                (sender as Button).Visibility = Visibility.Hidden;
            }
            if(potionSelector.SelectedItem!=null)
            {
                myModel.PotionUsed = potionSelector.SelectedItem as Models.PotionBase;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myModel.BattleStep(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            myModel.BattleStep(2);
        }
    }
}
