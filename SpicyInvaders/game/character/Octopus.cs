using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders.game.character
{
    class Octopus : Invader
    {

        public Octopus()
        {
            setColor(ConsoleColor.Blue);
            scoreGain = 40;
        }
    }
}
