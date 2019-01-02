// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 31.12.2018

using System;
using System.Threading;

namespace SpicyInvader.views
{
    /// <summary>
    /// Show informations about the conceptor of the game and legal credits.
    /// </summary>
    class AboutView : View
    {

        public override void onCreate(ScreenInfo screenInfo)
        {
            base.onCreate(screenInfo);

            // Display text
            int CursorPosY = 20;
            Console.SetCursorPosition(Console.CursorLeft, CursorPosY);

            string[] informationText = new string[]
            {
                "SPICY INVADER",
                "By Henoc Sese",
                "\n",
                "Music - Henoc Sese",
                "\n",
                "Programmer - Henoc Sese",
                "\n",
                "(c) Capsulo Inc."
            };

            foreach (string sentance in informationText)
            {
                int CursorPosX = (Console.WindowWidth / 2) - (sentance.Length / 2);
                Console.SetCursorPosition(CursorPosX, CursorPosY++);
                Console.WriteLine(sentance);
            }
            
        }

        public override void onResume()
        {
            base.onResume();

            // Management of keyboard press event
            this.keyboardEventHandler();
        }

        public override void onPause()
        {
            base.onPause();

            // Kills the keyboard press event's handler
            // Todo : eventThread.Join();
        }


        /// <summary>
        /// Handle keyboard press event safely
        /// </summary>
        private void keyboardEventHandler()
        {
            if (eventThread == null)
            {
                // Create a new Thread
                eventThread = new Thread(() =>
                {
                    while (State == configs.LifecycleState.RESUME)
                    {
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);

                            switch (key.Key)
                            {
                                case ConsoleKey.Escape:
                                    Program.Finish(this);
                                    break;
                            }
                        }
                    }
                });
            }

            // Start the thread
            eventThread.Start(); 
        }
    }
}
