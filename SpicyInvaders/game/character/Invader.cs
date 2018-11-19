using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpicyInvaders.game
{
    abstract class Invader : Character
    {
        private int row;
        private int column;
        protected int scoreGain; // The points gain by kill
        private bool isKilled;      // state of the invader, wether true or false

        public bool IsKilled { get => isKilled; set => isKilled = value; }

        void setRow()
        {

        }

        public void setColumn()
        {

        }

        public int getRow()
        {
            return row;
        }

        public int getColum()
        {
            return column;
        }

        /// <summary>
        /// Return the 
        /// </summary>
        /// <returns></returns>
        public int getScoreGain()
        {
            return scoreGain;
        }

        public void kill()
        {
            isKilled = true;
            const char CHAR_EMPTY = ' ';


            // Animation of the destruction
            for (int count = 0; count < 6; count++)
            {
                // pause the thread
                Thread.Sleep(50);

                if (count % 2 == 0)
                    Console.ForegroundColor = ConsoleColor.Black;
                else
                    Console.ForegroundColor = ConsoleColor.Yellow;

                // Rewrite the position
                Console.SetCursorPosition(getX(), getY());
                Console.Write(getAsciiCharacter());
            }

            // Erasing the ennemy displayed on screen)
            Console.SetCursorPosition(getX(), getY());
            Console.Write(CHAR_EMPTY);
        }
    }
}
