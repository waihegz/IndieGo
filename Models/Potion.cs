using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGo.Models
{
    public class PotionBase
    {
        protected string name;//name of the potion
        protected string description;//descriptioon of the potion effect
        protected string imagePath;//path to the image of the potion

        protected double moneyFactor;//the effect of the potion on the money made
        protected double defenceFactor;//the effect of the potion on the pokemon defense power
        protected double attackFactor; //the effect of the potion on the pokemon attack power;

        //methods for the above fields to be used for binding
        //As potions are static element INotifyPropertyChange interface is not required

        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }

        public string Description
        {
            get { return description; }
            protected set { description = value; }
        }
        public string ImagePath
        {
            get { return imagePath; }
            protected set { imagePath = value; }
        }

        public double MoneyFactor
        {
            get { return moneyFactor; }
            protected set { moneyFactor = value; }
        }
        public double DefenceFactor
        {
            get { return defenceFactor; }
            protected set { defenceFactor = value; }
        }
        public double AttackFactor
        {
            get { return attackFactor; }
            protected set { attackFactor = value; }
        }

        public override string ToString()
        {
            return this.name;
        }
    }


    public class Potion: PotionBase
    {
        public Potion()
        {
            Name = "Potion";
            Description = "This potion increases the player defence abilities.";
            ImagePath = "pack://application:,,,/PotionAssets/Potion.png";
            MoneyFactor = 1;
            DefenceFactor = 1.25;
            AttackFactor = 1;
        }
    }

    public class SuperPotion: PotionBase
    {
        public SuperPotion()
        {
            Name = "Super Potion";
            Description = "This potion increases the player attack abilities.";
            ImagePath = "pack://application:,,,/PotionAssets/Super_Potion.png";
            MoneyFactor = 1;
            DefenceFactor = 1;
            AttackFactor = 1.25;
        }
    }

    public class HyperPotion : PotionBase
    {
        public HyperPotion()
        {
            Name = "Hyper Potion";
            Description = "This potion increases the player defence abilities and the reward of winning battles.";
            ImagePath = "pack://application:,,,/PotionAssets/Hyper_Potion.png";
            MoneyFactor = 1.25;
            DefenceFactor = 1.35;
            AttackFactor = 1;
        }
    }


    public class MaxPotion : PotionBase
    {
        public MaxPotion()
        {
            Name = "Max Potion";
            Description = "This potion increases the player attack  abilities and the reward of winning battles.";
            ImagePath = "pack://application:,,,/PotionAssets/Max_Potion.png";
            MoneyFactor = 1.25;
            DefenceFactor = 1;
            AttackFactor = 1.35;
        }
    }


    static class PotionGenerator
    {
        static Random random = new Random();
        public static PotionBase Get()
        {
            int potionChoice = random.Next(1, 5);

            switch(potionChoice)
            {
                case 1:
                    return new Potion();
                case 2:
                    return new SuperPotion();
                case 3:
                    return new HyperPotion();
                case 4:
                    return new MaxPotion();
            }
            return new Potion();

        }
    }
}
