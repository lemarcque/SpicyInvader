// Author : Henoc Sese
// Description : Space Invaders game
// Date : 19.11.2018
// Locality : Lausanne 

using System;

namespace SpicyInvaders.domain.character
{
    class Ship : Character
    {


        public Ship():base(Camp.Allied)
        {
            missile = new Missile(this);
            Drawing = "▲";
            Color = ConsoleColor.Yellow;
        }

        /// <summary>
        /// Todo Overriding ...
        /// </summary>
        /// <param name="posX"></param>
        public override void SetX(int posX)
        {
            this.X = posX;
            missile.SetX(posX);
        }

        /// <summary>
        /// Todo Overriding ...
        /// </summary>
        /// <param name="posY"></param>
        public override void SetY(int posY)
        {
            this.Y = posY;
            missile.SetY(posY);
        }

        
    }
}
