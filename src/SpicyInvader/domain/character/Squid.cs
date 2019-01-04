using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders.domain.character
{
    class Squid : Invader
    {
        public Squid()
        {
            this.SetColor(ConsoleColor.White);
            scoreGain = 10;
        }
    }
}
