// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 31.12.2018

using SpicyInvader.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpicyInvader.views
{

    class OptionsView : View
    {
        
        private const int CursorPosX = 20;          // Base horizontal position for Console cursor
        private int CursorPosY = 20;                // Base vertical position for Console cursor
        private int currentMenuCursorPos;

        public override void onCreate(ScreenInfo screenInfo)
        {
            base.onCreate(screenInfo);

            // Display text
            

            Console.SetCursorPosition(CursorPosX, CursorPosY);
            Console.WriteLine("Sound : ON");

            CursorPosY += 2;
            Console.SetCursorPosition(CursorPosX, CursorPosY);
            Console.WriteLine("Level : " + Level.PADAWAN);

            currentMenuCursorPos = 1; 
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
                                case ConsoleKey.UpArrow:
                                    setCursorMenuPos(0);
                                    break;

                                case ConsoleKey.DownArrow:
                                    setCursorMenuPos(1);
                                    break;
                            }
                        }
                    }
                });
            }

            // Start the thread
            eventThread.Start();
        }

        private void setCursorMenuPos(int position)
        {
            currentMenuCursorPos = position;

            if (position == 0)
                CursorPosY = 20;
            else
                CursorPosY = 22;

            Console.SetCursorPosition(CursorPosX, CursorPosY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Co)
        }
    }
}
