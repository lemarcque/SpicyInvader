// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 05.01.2018

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
            Color = (ConsoleColor.DarkRed);
            scoreGain = 10;
        }
    }
}
