using System;

namespace SpicyInvaders
{
    /// <summary>
    /// Base class for the "Personnages" of the game
    /// </summary>
    abstract class Character
    {

        protected int posX;       // horizontal position of the character
        protected int posY;       // vertical position of the character
        protected int speedX;
        protected int speedY;
        protected int size;     
        public bool isAliveState;
        private String name;    // name of the character

        private String character = "█";
        private ConsoleColor color;

        private Environment environment;    // Environment in which the object is
        

        public Character()
        {
            // initialization
            isAliveState = true;
            speedX = 1;
            speedY = 0;
        }

        public Character(String name) :base()
        {
            this.name = name;
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
                    if(posX - speedX >= 0)
                        this.posX -= speedX;
                    break;
                case Direction.Right:
                    if (posX + speedX < environment.getWidth())
                        this.posX += speedX;
                    break;
                case Direction.Up:
                    if (posY - speedY > 0)
                        this.posY += speedY;
                    break;
                case Direction.Down:
                    if (posY + speedY < environment.getWidth())
                        this.posY += speedY;
                    break;
            }
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
