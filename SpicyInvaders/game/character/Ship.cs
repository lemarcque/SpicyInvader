// Author : Henoc Sese
// Description : Space Invaders game
// Date : 19.11.2018
// Locality : Lausanne 

using System;

namespace SpicyInvaders
{
    class Ship : Character
    {


        public Ship():base()
        {
            missile = new Missile(this);
            this.setAsciiCharacter("▲");
        }

        public Ship(String name) :base(name)
        {
            
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
