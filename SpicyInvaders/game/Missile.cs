using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    class Missile
    {


        // Variable
        public const int SPEED = 1;             // Speed of the missile
        private int posX;                       // horizontal position of the character
        private int posY;                       // vertical position of the character
        private Character owner;                // Owner of the missile (the one who shoot)
        public bool isMoving;

        private const String character = "█";   // The caracter associated with the character

        public Missile(Character owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Return the owner of the current missile
        /// </summary>
        /// <returns></returns>
        public Character getOwner()
        {
            return this.owner;
        }

        /// <summary>
        /// Return the horizontal position of the character
        /// </summary>
        /// <returns></returns>
        public int getX()
        {
            return posX;
        }

        /// <summary>
        /// Return the vertical position of the character
        /// </summary>
        /// <returns></returns>
        public int getY()
        {
            return posY;
        }

        public void setX(int posX)
        {
            this.posX = posX;
        }

        public void setY(int posY)
        {
            this.posY = posY;
        }
    }
}
