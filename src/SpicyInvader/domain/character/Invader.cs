// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2018

using System;
using System.Threading;

namespace SpicyInvaders.domain.character
{
    /// <summary>
    /// Represent an Invader
    /// </summary>
    abstract class Invader : Character
    {
        protected int row;
        protected int column;
        protected int scoreGain; // The points gain by kill
        private bool isKilled;      // state of the invader, wether true or false

        // Getter and Setters
        public bool IsKilled { get => isKilled; set => isKilled = value; }
        public int Column { get => column; set => column = value; }
        public int Row { get => row; set => row = value; }
        
        public Invader():base(character.Camp.Invader)
        {

        }
        /// <summary>
        /// Return the point associated with the invader
        /// when it is killed
        /// </summary>
        /// <returns></returns>
        public int GetScoreGain()
        {
            return scoreGain;
        }

        /// <summary>
        /// Make the state of the invader "killed"
        /// </summary>
        public void Kill()
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
                Console.SetCursorPosition(GetX(), GetY());
                Console.Write(Drawing);
            }

            // Erasing the ennemy displayed on screen)
            Console.SetCursorPosition(GetX(), GetY());
            Console.Write(CHAR_EMPTY);
        }
    }
}
