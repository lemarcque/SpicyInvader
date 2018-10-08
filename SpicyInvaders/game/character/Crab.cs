using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders.game.character
{
    class Crab : Invader
    {

        public Crab()
        {
            setColor(ConsoleColor.Yellow);
            scoreGain = 20;
        }
    }
}
