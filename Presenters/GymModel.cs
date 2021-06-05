using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace IndieGo.Presenters
{
    public class GymModel : INotifyPropertyChanged
    {

        #region fields and properties

        private string message; //provide updates on the battle

        private double moneyFactor = 1;
        private double attackFactor = 1;
        private double defenceFactor = 1;
        
        private Random random = new Random();

        private bool gameOver;

        private Models.PotionBase potionUsed;

        private Models.Pokemon gymHero;

        private Models.Pokemon playerPokemon;
        

        public string Message
        {
            get { return message; }
            private set 
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }
        

        public Models.Pokemon GymHero
        {
            get { return gymHero; }
            private set
            {
                gymHero = value;
                OnPropertyChanged("GymHero");
            }
        }

        public Models.Pokemon PlayerPokemon
        {
            get { return playerPokemon; }
            set 
            {
                if (playerPokemon == null &&  value!=null)
                {
                    Message = "Game Started";
                    playerPokemon = value;
                    OnPropertyChanged("PlayerPokemon");

                }
            }
        }
        

        public Models.PotionBase PotionUsed
        {
            get { return potionUsed; }
            set
            {
                if (value != null)
                {
                    potionUsed = value;
                    attackFactor = value.AttackFactor;
                    defenceFactor = value.DefenceFactor;
                    moneyFactor = value.MoneyFactor;
                }
                OnPropertyChanged("PotionChanged");
            }
        }
        #endregion
        
        
        public GymModel()
        {
            //all are set except player pokemon
            GymHero = generatePokemon();
            Message = "Add a pokemon to  start the battle";
            gameOver = false;


        }


        
        public int BattleStep(int attackChoice)
        {
            


            //validation
            if (gameOver == true)
                return -1;
            if (attackChoice != 1 && attackChoice != 2)
            {
                return -1; //not a valid input
            }
            if (PlayerPokemon == null)
            {
                Message = "Add player pokemon to start the battle";
                return -1;
            }

            //Message = "game logic";

            //player action
            string attackName=" "; //the player pokemon attack
            int attackPower;

            if (attackChoice==1)
            {
                attackName = PlayerPokemon.Attack1;
            }
            else if (attackChoice==2)
            {
                attackName = PlayerPokemon.Attack2;
            }

            attackPower = PlayerPokemon.GetAttacK(attackChoice);
            attackPower =  (int)(attackFactor * attackPower);



            //gymHero action
            int heroAttackChoice = random.Next(1, 3);
            string heroAttackName;
            int heroAttackPower = 0;

            if (heroAttackChoice == 1)
                heroAttackName = GymHero.Attack1;
            else
                heroAttackName = GymHero.Attack2;

            heroAttackPower = GymHero.GetAttacK(heroAttackChoice);
            heroAttackPower = (int)(heroAttackPower / defenceFactor);



            //game logic: 1) the player attack
            string messageBuffer = " ";
            if(attackPower >= GymHero.HP )
            {
                gameOver = true;
                Message = "you won!!";
                GymHero.AbsorbAttact(attackPower);
                winGame();

            }
            else
            {
                GymHero.AbsorbAttact(attackPower);
                messageBuffer += ("You attacked with " + attackName + " causing "+ attackPower.ToString()+" HP damage");

            }

            if(heroAttackPower>=PlayerPokemon.HP && gameOver!=true)
            {
                gameOver = true;
                Message = "you lost!!";
                PlayerPokemon.AbsorbAttact(heroAttackPower);
            }
            else if(gameOver != true)
            {
                PlayerPokemon.AbsorbAttact(heroAttackPower);
                messageBuffer += ("\n Gym attacked with " + heroAttackName + " causing " + heroAttackPower.ToString() + " HP damage");
                Message = messageBuffer;
            }

            return 1;
        }

        public void Heel()
        {
            if(PlayerPokemon!=null)
                PlayerPokemon.Heel();
        }

        private void winGame()
        {
            int stardust = (int)(20*moneyFactor);
            int pokeDollars = (int)(5*moneyFactor);
            Message = string.Format("You won the game {0} Stardust and {1} PokeDolalr were added to you account", stardust, pokeDollars);
            Models.Player myplayer = Models.Player.Instance;
            myplayer.StarDust += stardust;
            myplayer.PokeDollar += pokeDollars;
        }
        private Models.Pokemon generatePokemon()
        {
            Models.Pokemon newPokemon = Models.PokemonGenerator.Get();

            int stageMax =  random.Next(30, 50);

            for(int i =0;  i<stageMax; i++)
            {
                newPokemon.PowerUp();
            }
            for(int i=0; i<4; i++)
            {
                newPokemon.Evolve();
            }
            return newPokemon;
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
