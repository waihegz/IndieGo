using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGo.Models
{
    public class PickaPokemon : Pokemon
    {
        public PickaPokemon()
        {
            Species = "Picka";//specify the species name
            MaxLevel = 2; //specify the maximum level
            Evolve(); //evolve the pokemon to the first level

        }



        public override bool Evolve()
        {
            if (CurLevel == MaxLevel)
                return false;

            switch (CurLevel)
            {
                case 0:
                    Attack1 = "Sudden Spark";
                    Attack2 = "Taser Tail";
                    ImagePath = "pack://application:,,,/PokemonAssets/Picka/1.png";
                    break;
                case 1:
                    Attack1 = "Thunder";
                    Attack2 = "Electric Grid";
                    ImagePath = "pack://application:,,,/PokemonAssets/Picka/2.png";
                    break;
            }

            CurLevel++;
            return true;
        }

    }
    public class MousePokemon : Pokemon
    {
        public MousePokemon()
        {
            Species = "Mouse";//specify the species name
            MaxLevel = 2; //specify the maximum level
            Evolve(); //evolve the pokemon to the first level

        }



        public override bool Evolve()
        {
            if (CurLevel == MaxLevel)
                return false;

            switch (CurLevel)
            {
                case 0:
                    Attack1 = "Tackle";
                    Attack2 = "Tail Whip";
                    ImagePath = "pack://application:,,,/PokemonAssets/Mouse/1.png";
                    break;
                case 1:
                    Attack1 = "Fangs Crush";
                    Attack2 = "Quick Claws";
                    ImagePath = "pack://application:,,,/PokemonAssets/Mouse/2.png";
                    break;
            }

            CurLevel++;
            return true;
        }

    }
    public class TurtlePokemon : Pokemon
    {
        public TurtlePokemon()
        {
            Species = "Turtle";//specify the species name
            MaxLevel = 3; //specify the maximum level
            Evolve(); //evolve the pokemon to the first level

        }


        public override bool Evolve()
        {
            if (CurLevel == MaxLevel)
                return false;

            switch (CurLevel)
            {
                case 0:
                    Attack1 = "Water Gun";
                    Attack2 = "Rain Dance";
                    ImagePath = "pack://application:,,,/PokemonAssets/Turtle/1.png";
                    break;
                case 1:
                    Attack1 = "Aqua Tail";
                    Attack2 = "Hydro Pump";
                    ImagePath = "pack://application:,,,/PokemonAssets/Turtle/2.png";
                    break;
                case 2:
                    Attack1 = "Tsunami";
                    Attack2 = "Deep Sea";
                    ImagePath = "pack://application:,,,/PokemonAssets/Turtle/3.png";
                    break;

            }

            CurLevel++;
            return true;
        }

    }
    public class LizardPokemon : Pokemon
    {
        public LizardPokemon()
        {
            Species = "Lizard";//specify the species name
            MaxLevel = 3; //specify the maximum level
            Evolve(); //evolve the pokemon to the first level

        }



        public override bool Evolve()
        {
            if (CurLevel == MaxLevel)
                return false;

            switch (CurLevel)
            {
                case 0:
                    Attack1 = "Bake a Cake";
                    Attack2 = "Fire Spin";
                    ImagePath = "pack://application:,,,/PokemonAssets/Lizard/1.png";
                    break;
                case 1:
                    Attack1 = "Fire Punch";
                    Attack2 = "Flamethrower";
                    ImagePath = "pack://application:,,,/PokemonAssets/Lizard/2.png";
                    break;
                case 2:
                    Attack1 = "Flare Blitz";
                    Attack2 = "Inferno";
                    ImagePath = "pack://application:,,,/PokemonAssets/Lizard/3.png";
                    break;

            }

            CurLevel++;
            return true;
        }

    }
    public class SeedPokemon : Pokemon
    {
        public SeedPokemon()
        {
            Species = "Seed";//specify the species name
            MaxLevel = 3; //specify the maximum level
            Evolve(); //evolve the pokemon to the first level

        }
        


        public override bool Evolve()
        {
            if (CurLevel == MaxLevel)
                return false;

            switch(CurLevel)
            {
                case 0:
                    Attack1 = "Vine Whip";
                    Attack2 = "Bullet Seed";
                    ImagePath = "pack://application:,,,/PokemonAssets/Seed/1.png";
                    break;
                case 1:
                    Attack1 = "Razor Seed";
                    Attack2 = "Seed Bomb";
                    ImagePath = "pack://application:,,,/PokemonAssets/Seed/2.png";
                    break;
                case 2:
                    Attack1 = "Solar Beam";
                    Attack2 = "Leaf Storm";
                    ImagePath = "pack://application:,,,/PokemonAssets/Seed/3.png";
                    break;

            }

            CurLevel++;
            return true;
        }

    }
}
