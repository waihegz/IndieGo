using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace IndieGo.Presenters
{
    class InventoryModel : INotifyPropertyChanged
    { 
        /// <summary>
        /// currently implemented
        /// message storage
        /// player storage
        /// </summary>
        private Selector mySelector; //secletor : combobox to indicate currently used pokemon
        private Models.Player myPlayer;//player instance
        private string message;
        
        public Models.Player MyPlayer
        {
            get { return myPlayer; }
            set 
            {
                myPlayer = value;
                OnPropertyChanged("MyPlayer");
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        #region Methods

        public void Evolve()
        {
            //get price of evolving

            Models.Pokemon myPokemon = ((Models.Pokemon)mySelector.SelectedItem);
            if (myPokemon == null)
            {
                Message = "Please select a pokemon";
                return;
            }
            int[] price = ((Models.Pokemon)mySelector.SelectedItem).EvolveCost;
            //check if pokemon can evovle further
            if (myPokemon.CurLevel >= myPokemon.MaxLevel)
            {
                Message = "Pokemon reached maximum evolution level";
                return;
            }
      
            //check player balance
            if(MyPlayer.PokeDollar>= price[0]  && MyPlayer.StarDust>=price[1])
            {
                MyPlayer.PokeDollar -= price[0];
                MyPlayer.StarDust -= price[1];
                myPokemon.Evolve();
                Message = "";
            }
            else
            {
                Message = "You dont have enough funds";
            }
        }

        public void Sell()
        {
            Models.Pokemon myPokemon = ((Models.Pokemon)mySelector.SelectedItem);
            if(myPokemon ==null)
            {
                Message = "Please select a pokemon";
                return;
            }
            myPlayer.PokeDollar+= myPokemon.CurLevel * 3;
            myPlayer.StarDust += myPokemon.Stage * 5;
            myPlayer.MyCharachers.Remove(myPokemon);
        }

        public void PowerUp()
        {
            //get price of evolving

            Models.Pokemon myPokemon = ((Models.Pokemon)mySelector.SelectedItem);
            if (myPokemon == null)
            {
                Message = "Please select a pokemon";
                return;
            }
            int[] price = ((Models.Pokemon)mySelector.SelectedItem).PowerUpCost;

            if (myPokemon == null)
            {
                Message = "Please select a pokemon";
                return;
            }

            //check player balance
            if (MyPlayer.PokeDollar >= price[0] && MyPlayer.StarDust >= price[1])
            {
                MyPlayer.PokeDollar -= price[0];
                MyPlayer.StarDust -= price[1];
                myPokemon.PowerUp();
                Message = "";
            }
            else
            {
                Message = "You dont have enough funds";
            }
        }
        
        public void Rename(string newName)
        {
            Models.Pokemon myPokemon = ((Models.Pokemon)mySelector.SelectedItem);
            if (myPokemon == null)
            {
                Message = "Please select a pokemon";
                return;
            }
            myPokemon.Name = newName;
        }
        #endregion

        #region Singelton Construction
        private static InventoryModel instance;

        public static InventoryModel  Instance(Selector mySelector, Models.Player player)
        {
            if (instance == null)
                instance = new InventoryModel(mySelector, player);
            return instance;
        }

        private InventoryModel(Selector mySelector, Models.Player player)
        {
            this.mySelector = mySelector;
            this.myPlayer = player;
            this.Message = "";
        }
        #endregion

        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
