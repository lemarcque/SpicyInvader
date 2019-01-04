// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2018

using SpicyInvaders.domain.character;
using System;

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
        public Character GetOwner()
        {
            return this.owner;
        }

        /// <summary>
        /// Return the horizontal position of the character
        /// </summary>
        /// <returns></returns>
        public int GetX()
        {
            return posX;
        }

        /// <summary>
        /// Return the vertical position of the character
        /// </summary>
        /// <returns></returns>
        public int GetY()
        {
            return posY;
        }

        public void SetX(int posX)
        {
            this.posX = posX;
        }

        public void SetY(int posY)
        {
            this.posY = posY;
        }
    }
}
