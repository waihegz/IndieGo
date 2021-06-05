using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGo.Models
{
    public class Player: INotifyPropertyChanged 
    {
        private int pokeDollar;
        public int PokeDollar
        {
            get { return pokeDollar;}
            set 
            {
                pokeDollar = value;
                OnPropertyChanged("PokeDollar");
            }
        }

        private int starDust;
        public int StarDust
        {
            get { return starDust; }
            set
            {
                starDust  = value;
                OnPropertyChanged("StarDust");
            }
        }

        private ObservableCollection<Pokemon> myCharachters;

        public  ObservableCollection<Pokemon> MyCharachers
        {
            get { return myCharachters; }
            private set { myCharachters = value; }
        }


        private ObservableCollection<PotionBase> myPotions;

        public ObservableCollection<PotionBase> MyPotions
        {
            get { return myPotions; }
            private set { myPotions = value; }
        }

        private Player()
        {
            starDust = 1000;
            pokeDollar = 1000;
            myCharachters = new ObservableCollection<Pokemon>();
            myPotions = new ObservableCollection<PotionBase>();

        }
        static private Player instance;
        static public Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();
                return instance;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
