// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 31.12.2018

using SpicyInvader.models;
using SpicyInvader.views.utils;
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
        
        private int baseCursorPosX { get; }          // Base horizontal position for Console cursor
        private int CursorPosY;                // Base vertical position for Console cursor
        private int currentMenuCursorPos;
        private string[] strMenuLines;              // All text for game's options that will be printed on the console
        private Level currentLevel;                 // The current level choose by the player
        private bool isSoundActivated;

        public OptionsView()
        {
            baseCursorPosX = (Console.WindowWidth / 2) - (Character.SELECT_CURSOR.ToString().Length + 5 + 7);
            CursorPosY = 20;
        }

        public override void onCreate(ScreenInfo screenInfo)
        {
            base.onCreate(screenInfo);

            // Set variables and options
            strMenuLines = new string[]{
                "Sound",
                "Level"
            };

            currentLevel = Level.PADAWAN;
            isSoundActivated = false;

            // Display text

            // Set the position of the menu cursor
            setCursorMenuPos(0);
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

                                case ConsoleKey.Spacebar:
                                    switchOptionValue();
                                    setCursorMenuPos(currentMenuCursorPos);
                                    break;

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

        /// <summary>
        /// Intervert the value of an optin
        /// </summary>
        private void switchOptionValue()
        {
            switch(currentMenuCursorPos)
            {
                case 0:
                    isSoundActivated = !isSoundActivated;
                    break;

                case 1:
                    currentLevel = (currentLevel == Level.JEDI) ? Level.PADAWAN : Level.JEDI;
                    break;
            }
        }

        private void setCursorMenuPos(int position)
        {
            currentMenuCursorPos = position;
            String sentance;

            // Reset the color to the initial (gray)
            Console.ForegroundColor = ConsoleColor.White;

            if (position == 0)
            {
                // Reset color for the previous line
                Console.SetCursorPosition(baseCursorPosX, CursorPosY);
                Console.WriteLine(Character.SPACE);

                Console.SetCursorPosition(baseCursorPosX + 5, 22);
                Console.WriteLine(strMenuLines[1] + " : " + currentLevel);
                
                // Change position for sound's option
                CursorPosY = 20;
                string soundValue = (isSoundActivated) ? "ON" : "OFF";
                sentance = strMenuLines[0] + " : " + soundValue;
            }
            else
            {
                // Reset color for the previous line
                Console.SetCursorPosition(baseCursorPosX, CursorPosY);
                Console.WriteLine(Character.SPACE);

                Console.SetCursorPosition(baseCursorPosX + 5, 20);
                string currentSoundValue = (isSoundActivated) ? "ON" : "OFF";
                Console.WriteLine(strMenuLines[0] + " : " + currentSoundValue);

                // Change position for levels option
                CursorPosY = 22;
                sentance = strMenuLines[1] + " : " + currentLevel;
            }

            // Print cursor arrow character '>'
            Console.SetCursorPosition(baseCursorPosX, CursorPosY);
            ConsoleUtils.ClearCurrentConsoleLine();
            Console.WriteLine(Character.SELECT_CURSOR.ToString());

            // Print sentance for the current menu's selection
            Console.SetCursorPosition(baseCursorPosX + 5, CursorPosY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(sentance);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
