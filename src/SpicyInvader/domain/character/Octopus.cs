// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders.domain.character
{
    class Octopus : Invader
    {

        public Octopus()
        {
            SetColor(ConsoleColor.Blue);
            scoreGain = 40;
        }
    }
}
