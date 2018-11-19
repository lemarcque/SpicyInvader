// Author : Henoc Sese
// Description : Character of the game
// Date : 12.11.2018


using System;
using System.Diagnostics;

namespace SpicyInvaders
{
    /// <summary>
    /// Base class for the "Personnages" of the game
    /// </summary>
    abstract class Character
    {

        protected int posX;       // horizontal position of the character
        protected int posY;       // vertical position of the character
        protected int speedX;     // The horizontal speed of the character
        protected int speedY;     // The vertical speed of the character
        protected int size;       
        public bool isAliveState;
        private string name;    // name of the character

        // Propreties of a shooter
        private bool isShooting;            // determine if the missile moving
        protected Missile missile;                    // The ship has only one missile


        private string character = "█";
        private ConsoleColor color;

        private Environment environment;    // Environment in which the object is
        

        public Character()
        {
            // initialization
            isAliveState = true;
            speedX = 1;
            speedY = 1;
            missile = new Missile(this);
        }

        public Character(String name) :base()
        {
            this.name = name;
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
        /// Override the method toString
        /// </summary>
        /// <returns></returns>
        public String toString()
        {
            return name;
        }

        /// <summary>
        /// Return the name of the character
        /// </summary>
        /// <returns></returns>
        public String getName()
        {
            return name;
        }

        /// <summary>
        /// Return the ASCII char to display
        /// </summary>
        /// <returns></returns>
        public String getAsciiCharacter()
        {
            return character;
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

        public virtual void setX(int posX)
        {
            this.posX = posX;
        }

        public virtual void setY(int posY)
        {
            this.posY = posY;
        }

        public void setSize(int size)
        {
            this.size = size;
        }

        public virtual void setLife(bool isAlive)
        {
            this.isAliveState = isAlive;
        }

        public bool isAlive()
        {
            return isAliveState;
        }

        public void move(Direction direction)
        {
            switch(direction)
            {
                case Direction.Left:
                {
                    if (posX - speedX >= 0)
                        this.posX -= speedX;
                    break;
                }
                
                case Direction.Right:
                {
                    if (posX + speedX < environment.getWidth())
                        this.posX += speedX;
                    break;
                }
                   
                case Direction.Up:
                {
                    if (posY - speedY > 0)
                        this.posY += speedY;
                    break;
                }
                    
                case Direction.Down:
                    {
                        if (posY + speedY < environment.getHeight())
                            this.posY += speedY;
                        break;
                    }
            }
        }

        public bool OnCollision(Character p2)
        {
            return getX() == p2.getX() && getY() == p2.getY();
        }

        public bool onHitboxCollision(Character p2)
        {
            const short HITBOX_WIDTH = 1;         // zone of collision

            // collision de haut en bas
            if ((this.getX() == p2.getX()) && (this.getY() - p2.getY() == HITBOX_WIDTH))
            {
                Debug.WriteLine("1");
                return true;
            }

            // collision de bas en haut
            if ((this.getX() == p2.getX()) && (p2.getY() - this.getY() == HITBOX_WIDTH))
            {
                Debug.WriteLine("2");
                return true;
            }

            // collision de gauche à droite
            if ((this.getX() - p2.getX()) == HITBOX_WIDTH && (p2.getY() == this.getY()))
            {
                Debug.WriteLine("3");
                return true;
            }

            // collision de droite à gauche
            if ((p2.getX() - this.getX()) == HITBOX_WIDTH && (p2.getY() == this.getY()))
            {
                Debug.WriteLine("4");
                return true;
            }
                

            // collision à l'intérieur
            if (this.getX() == p2.getX() && this.getY() == p2.getY())
                return true;

            return false;
        }


        protected void setColor(ConsoleColor color)
        {
            this.color = color;
        }

        public void setAsciiCharacter(String character)
        {
            this.character = character;
        }

        public void setEnvironment(Environment environment)
        {
            this.environment = environment;
        }

    }
}
