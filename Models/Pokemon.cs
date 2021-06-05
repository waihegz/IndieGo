using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGo.Models
{

    /// <summary>
    /// This namespace contains the design of the pokemon abstract class 
    /// 
    /// How to add more pokemons to the game?
    /// 
    /// Design of The class
    /// </summary>
    /// 
    
 
    public abstract class Pokemon : INotifyPropertyChanged 
    {
        #region non-editable visible by outside classes  
        protected int curLevel; //current level of the pokemon (related to evolution)
        protected int maxLevel; //maximum evolutionairy level of the pokemon
        protected int hP;       //health points of the pokemon (related to evolution)
        protected int stage;    //current stage of the pokemon related to the powerup (increase HP and base attack power) 
        protected string imagePath;//store the image of the pokemon
        protected string species;//the type of the pokemon (fixed_
        protected string name; //name of pokemon: editable by the user 
        protected string attack1; // name of first attack of the pokemon changes in Evolve
        protected string attack2; // name of second attack of the pokemon changes in Evolve
        protected int[] evolveCost; //price of evolving first index is PokeDollar second is StartDust
        protected int[] powerUpCost;

        public int CurLevel
        {
            get { return curLevel; }
            protected set
            {
                curLevel = value;
                OnPropertyChanged("CurLevel");
            }
        }

        public int MaxLevel
        {
            get { return maxLevel; }
            protected set
            {
                maxLevel = value;
                OnPropertyChanged("MaxLevel");
            }
        }

        public int HP
        {
            get { return hP; }
            protected set
            {
                hP = value;
                OnPropertyChanged("HP");
            }
        }
        
        public int Stage
        {
            get { return stage; }
            protected set
            {
                stage = value;
                OnPropertyChanged("Stage");
            }

        }

        public string ImagePath
        {
            get { return imagePath; }
            protected set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }


        public string Species
        {
            get { return species; }
            protected set
            {
                //this should not be used in current implementation but implemented for future proofing 
                species = value;
                OnPropertyChanged("Species");
            }
        }

        public string Name
        {
            get
            {  if (name == null)
                    return species;
                return name; 
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Attack1
        {
            get { return attack1; }
            protected set
            {
                attack1 = value;
                OnPropertyChanged("Attack1");
            }
        }
        public string Attack2
        {
            get { return attack2; }
            protected set
            {
                attack2 = value;
                OnPropertyChanged("Attack2");
            }
        }


        public int[] EvolveCost
        {
            get
            {
                return evolveCost;
            }
            private set
            {
                evolveCost = value;
                OnPropertyChanged("EvolveCost");
            }
        }


        public int[] PowerUpCost
        {
            get
            {
                return powerUpCost;
            }
            private set
            {
                powerUpCost = value;
                OnPropertyChanged("PowerUpCost");
            }
        }
        #endregion



        #region non-visible, non-editable by outside classes
        //Properties are not designed for these classes as data binding to UI elements is not expected
        protected int baseAttackPower; //attack power 
        protected int baseHP;
        protected Random random;
        protected string baseDirectory;

        #endregion

        //constructor
        public Pokemon()
        {
            //intialise essentail starting points
            random = new Random();
            CurLevel = 0;
            Stage = 1;

            //default settings can be overwritten by class users
            HP = 50;
            baseHP = HP;
            baseAttackPower = 50;
            EvolveCost = new int[2] { 10, 10 };
            PowerUpCost= new int[2] { 2, 10 };

        }

        public override string ToString()
        {
            return Name;
        }

        //abstract methods 
        public virtual int  GetAttacK(int choice)
        {
            //This is a rought formula for calculating  attack power it can be overwritten by class users as needed 
            //choice indicated wether attack 1 or attack 2 where chosen 
            //attack 2 is risker and potientially more powerful
            if (choice == 1)
            {
                int total = baseAttackPower * CurLevel * random.Next(3, 6);
                return (int)(total / 50);//normalise to maintain long enough game play
            }
            else if (choice == 2)
            {
                int total = baseAttackPower * CurLevel * random.Next(1, 10);
                return (int)(total / 50);
            }
            else
            {
                return 0;
            }
        }

        public virtual void PowerUp()
        {
            //a rough template for powerup can be override by class users as needed 
            Stage++;
            baseHP += 5;
            baseAttackPower += 5;
            HP = baseHP;
        }

        public abstract bool Evolve();
        public  virtual int AbsorbAttact(int power)
        {
            int newHP = HP- power;
            HP = Math.Max(newHP, 0);
            return HP;
           
        }
        public virtual void Heel()
        {
            HP = baseHP;
        }

        //interface
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
