// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 06.01.2019

using SpicyInvader.views.utils;
using System;
using System.Threading;

namespace SpicyInvader.views
{

    /// <summary>
    /// Diplay message of the state of the game
    /// </summary>
    class GameFinish : View
    {

        string message;   // The message that will be displayed

        public GameFinish(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// The view is displayed on the screen and ready to be manipulated.
        /// </summary>
        public override void onResume()
        {
            base.onResume();

            // Show the message
            int posX = Program.Width / 2 - message.Length / 2;
            int posY = Program.Height / 2;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(message);

            // Make the program in pause mdoe
            Thread.Sleep(3000);

            // Quit the application
            Program.Exit();
        }
    }
}
