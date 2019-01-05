// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2018

using System;

namespace SpicyInvader.domain
{
    public class DisplayableObject
    {
        public int X { get; set; }       // horizontal position of the character
        public int Y { get; set; }       // vertical position of the character
        public string Drawing { get; set; }     // ASCIII representing the character (player) 
        public ConsoleColor Color { get; set; }         // Color of the character ascii

        public override string ToString()
        {
            return Drawing;
        }
    }
}
