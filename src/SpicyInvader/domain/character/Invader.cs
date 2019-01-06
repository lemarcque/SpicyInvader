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
    public abstract class Invader : Character
    {
        protected int row;
        protected int column;
        protected int scoreGain; // The points gain by kill
        private bool isKilled;      // state of the invader, wether true or false

        // Getter and Setters
        public bool IsKilled { get => isKilled; set => isKilled = value; }
        public int Column { get => column; set => column = value; }
        public int Row { get => row; set => row = value; }

        private static readonly object ConsoleWriterLock = new object();

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
            IsAlive = false;
        }
    }
}
