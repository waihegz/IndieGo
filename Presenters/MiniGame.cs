using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGo.Presenters
{
    public class MiniGame : INotifyPropertyChanged
    {
        Random random = new Random();

        bool isPokemon; //indicate if the catched item is a potion or a pokemon

        string imagePath; //to display the image of the catched object


        public string ImagePath
        {
            get { return imagePath; }
            private set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }


        string message1; //to display the message for the first game prompt (i.e do you wanna play)
        
        public string Message1
        {
            get { return message1; }
            private set 
            {
                message1 = value;
                OnPropertyChanged("Message1");
            }
        }


        string message2; //to display the prompty for typing game

        public string Message2
        {
            get { return message2; }
            private set
            {
                message2 = value;
                OnPropertyChanged("Message2");
            }
        }

        private string toType;//string for the typing game

        public string ToType
        {
            get { return toType; }
            private set { toType = value; }
        }

        Models.Pokemon pokemonCatch; //represent potiential pokemon to catch
        Models.PotionBase potionCatch;//represent potiential potion to catch
        //the name and the image will be displayer in the message and through imagePath so no need
        //to set up properties 

        public MiniGame()
        {
            //set up message1 and image path 
            string name = "";//name of captured item
            string description = "";//description of captured item if applicable
            int randomChoice = random.Next(0, 10);

            if (randomChoice < 6)
                //the catched item is a pokemon (potions are more rare)
            {
                isPokemon = true;
                pokemonCatch = Models.PokemonGenerator.Get();
                ImagePath = pokemonCatch.ImagePath;
                name = pokemonCatch.Name;

            }
            else
            {
                isPokemon = false;
                potionCatch = Models.PotionGenerator.Get();
                ImagePath = potionCatch.ImagePath;
                name = potionCatch.Name;
                description = potionCatch.Description;
            }

            Message1 = string.Format("You found {0}. Do you want to catch it? \n{1}",name, description);
        }
        private string GenerateString(int length)
        {

            // credit: https://www.educative.io/edpresso/how-to-generate-a-random-string-in-c-sharp
            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }


        public void StartGame()
        {
            //generate to type and the message 2 string
            toType = GenerateString(8);
            Message2 = string.Format("Type the following string : {0}", toType);
        }
       
        public void WinGame()
        {
            //add some startdust , pokedollar and the item to the player
            string name = "";
            if (isPokemon == true)
            {
                name = pokemonCatch.Name;
                Models.Player.Instance.MyCharachers.Add(pokemonCatch);
            }
            else
            {
                name = potionCatch.Name;
                Models.Player.Instance.MyPotions.Add(potionCatch);
            }
            Models.Player.Instance.StarDust += 10;
            Models.Player.Instance.PokeDollar += 3;
            //congratulation message
            Message2 = string.Format("Congratulation!! 10 Stardust, 3 Pokedollar, and {0} were added to your account", name); 
        }

        public void EndGame(bool  won= true)
        {
            if (won)
            {
                Message1 = "Congratulations";
            }
            else
            {
                Message2 = " ";
                Message1 = "You lost..Better luck next time";
                
            }

            
        }

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
