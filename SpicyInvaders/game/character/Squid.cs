using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders.game.character
{
    class Squid : Invader
    {
        public Squid()
        {
            setColor(ConsoleColor.White);
            scoreGain = 10;
        }
    }
}
