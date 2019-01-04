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
    class Crab : Invader
    {

        public Crab()
        {
            SetColor(ConsoleColor.Yellow);
            scoreGain = 20;
        }

        /// <summary>
        /// Shoot action
        /// Override the method frim the superclass
        /// </summary>
        /// <param name="state"></param>
        public virtual void shoot(bool state)
        {
            base.Shoot(state);
            missile.SetY(GetY() + 2);
        }
    }
}
