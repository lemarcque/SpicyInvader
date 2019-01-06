// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2018


using SpicyInvader;
using SpicyInvader.domain;
using System;
using System.Diagnostics;

namespace SpicyInvaders.domain.character
{
    /// <summary>
    /// Base class for the "Personnages" of the game
    /// </summary>
    public abstract class Character : DisplayableObject
    {

        public int SpeedX { get; set; }     // The horizontal speed of the character
        protected int speedY;     // The vertical speed of the character
        protected int size;       
        public bool isAliveState;
        private string name;    // name of the character
        public Camp Camp { get; }      // The group of which the character belong
        

        // Propreties of a shooter
        private bool isShooting;            // determine if the missile moving
        protected Missile missile;                    // The ship has only one missile




        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="camp"></param>
        public Character(Camp camp)
        {
            // initialization
            isAliveState = true;
            SpeedX = 1;
            speedY = 1;
            Drawing = "█";
            missile = new Missile(this);
            this.Camp = camp;
        }


        /// <summary>
        /// Shoot action
        /// </summary>
        /// <param name="state"></param>
        public virtual void Shoot(bool state)
        {
            isShooting = state;

            missile.isMoving = state;
            missile.X = X;
            missile.Y = Y - 2;
        }

        /// <summary>
        /// Return a boolean 
        /// </summary>
        /// <returns></returns>
        public bool GetIsShooting
        {
            get { return isShooting; }
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
        public override String ToString()
        {
            return Drawing;
        }

        /// <summary>
        /// Return the name of the character
        /// </summary>
        /// <returns></returns>
        public String GetName()
        {
            return name;
        }
        /// <summary>
        /// Return the horizontal position of the character
        /// </summary>
        /// <returns></returns>
        public int GetX()
        {
            return X;
        }

        /// <summary>
        /// Return the vertical position of the character
        /// </summary>
        /// <returns></returns>
        public int GetY()
        {
            return Y;
        }

        public virtual void SetX(int posX)
        {
            this.X = posX;
            // Test ...
        }

        public virtual void SetY(int posY)
        {
            this.Y = posY;
        }

        public void SetSize(int size)
        {
            this.size = size;
        }

        public virtual void SetLife(bool isAlive)
        {
            this.isAliveState = isAlive;
        }

        public bool IsAlive { get => isAliveState; set => isAliveState = value; }


        public bool isAlive()
        {
            return isAliveState;
        }

        public void Move(Direction direction)
        {
            switch(direction)
            {
                case Direction.Left:
                {
                    if (X - SpeedX >= 0)
                        this.X -= SpeedX;
                    break;
                }
                
                case Direction.Right:
                {
                    if (X + SpeedX < Program.Width)
                        this.X += SpeedX;
                    break;
                }
                   
                case Direction.Up:
                {
                    if (Y - speedY > 0)
                        this.Y += speedY;
                    break;
                }
                    
                case Direction.Down:
                    {
                        if (Y + speedY < Program.Height)
                            this.Y += speedY;
                        break;
                    }
            }
        }

        public bool OnCollision(Character p2)
        {
            return GetX() == p2.GetX() && GetY() == p2.GetY();
        }

        public bool OnHitboxCollision(Character p2)
        {
            const short HITBOX_WIDTH = 1;         // zone of collision

            // collision de haut en bas
            if ((this.GetX() == p2.GetX()) && (this.GetY() - p2.GetY() == HITBOX_WIDTH))
            {
                Debug.WriteLine("1");
                return true;
            }

            // collision de bas en haut
            if ((this.GetX() == p2.GetX()) && (p2.GetY() - this.GetY() == HITBOX_WIDTH))
            {
                Debug.WriteLine("2");
                return true;
            }

            // collision de gauche à droite
            if ((this.GetX() - p2.GetX()) == HITBOX_WIDTH && (p2.GetY() == this.GetY()))
            {
                Debug.WriteLine("3");
                return true;
            }

            // collision de droite à gauche
            if ((p2.GetX() - this.GetX()) == HITBOX_WIDTH && (p2.GetY() == this.GetY()))
            {
                Debug.WriteLine("4");
                return true;
            }
                

            // collision à l'intérieur
            if (this.GetX() == p2.GetX() && this.GetY() == p2.GetY())
                return true;

            return false;
        }
        
    }
}
