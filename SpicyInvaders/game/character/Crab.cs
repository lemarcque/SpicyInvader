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

        /// <summary>
        /// Shoot action
        /// Override the method frim the superclass
        /// </summary>
        /// <param name="state"></param>
        public virtual void shoot(bool state)
        {
            base.shoot(state);
            missile.setY(getY() + 2);
        }
    }
}
