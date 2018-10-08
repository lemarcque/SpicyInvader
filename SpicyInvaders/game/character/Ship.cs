using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    class Ship : Character
    {
        private bool isShooting;            // determine if the missile moving
        Missile missile;                    // The ship has only one missile

        public Ship()
        {
            missile = new Missile(this);
            this.setAsciiCharacter("▲");
        }

        public Ship(String name) :base(name)
        {
            missile = new Missile(this);
        }

        /// <summary>
        /// Shoot action
        /// </summary>
        /// <param name="state"></param>
        public void shoot(bool state)
        {
            isShooting = state;

            missile.isMoving = state;
            missile.setX(posX);
            missile.setY(posY - 2);
        }

        public bool getIsShooting()
        {
            return isShooting;
        }

        /// <summary>
        /// Return the missile object
        /// </summary>
        /// <returns></returns>
        public Missile GetMissile()
        {
            return missile;
        }

        /// <summary>
        /// Todo Overriding ...
        /// </summary>
        /// <param name="posX"></param>
        public override void setX(int posX)
        {
            this.posX = posX;
            missile.setX(posX);
        }

        /// <summary>
        /// Todo Overriding ...
        /// </summary>
        /// <param name="posY"></param>
        public override void setY(int posY)
        {
            this.posY = posY;
            missile.setY(posY);
        }

        
    }
}
