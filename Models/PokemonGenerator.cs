using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGo.Models
{
    //return a random pokemon
    static class PokemonGenerator
    {
        private static Random random = new Random();
        private static int numClasses = 5;
        public static Pokemon Get()
        {
            int choice = random.Next(1, numClasses + 1);
            switch(choice)
            {
                case 1:
                    return new LizardPokemon();
                case 2:
                    return new MousePokemon();
                case 3:
                    return new TurtlePokemon();
                case 4:
                    return new SeedPokemon();
                case 5:
                    return new PickaPokemon();

            }
            return new TurtlePokemon();
        }
    }
}
