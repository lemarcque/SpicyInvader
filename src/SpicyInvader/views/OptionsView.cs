// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 31.12.2018

using SpicyInvader.models;
using SpicyInvader.presenters;
using SpicyInvader.views.utils;
using System;
using System.Threading;

namespace SpicyInvader.views
{

    class OptionsView : View
    {

        public OptionsPresenter Presenter { get; set; }        // Reference of the Presenter

        private int baseCursorPosX { get; }             // Base horizontal position for Console cursor
        private int CursorPosY;                         // Base vertical position for Console cursor
        private int currentMenuCursorPos;
        private string[] strMenuLines;                  // All text for game's options that will be printed on the console

        public OptionsView()
        {
            baseCursorPosX = (Console.WindowWidth / 2) - (Char.SELECT_CURSOR.ToString().Length + 5 + 7);
            CursorPosY = 20;
        }

        /// <summary>
        /// Creation of the View object
        /// </summary>
        public override void onCreate(ScreenInfo screenInfo)
        {
            base.onCreate(screenInfo);

            // Set variables and options
            strMenuLines = new string[]{
                "Sound",
                "Level"
            };

            // Display text

            // Set the position of the menu cursor
            setCursorMenuPos(0);
        }

        /// <summary>
        /// The view is displayed on the screen and ready to be manipulated.
        /// </summary>
        public override void onResume()
        {
            base.onResume();

            // Management of keyboard press event
            this.keyboardEventHandler();
        }

        /// <summary>
        /// The View lose its focus and enter in to the paused state.
        /// </summary>
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

                                    String soundModeValue = (Presenter.getSoundModeActivated()) ? "ON" : "OFF";
                                    Presenter.updateGameOptions(soundModeValue, Presenter.getCurrentLevel());
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
                    Presenter.switchSoundMode();
                    break;

                case 1:
                    Presenter.switchCurrentLevel();
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
                Console.WriteLine(Char.SPACE);

                Console.SetCursorPosition(baseCursorPosX + 5, 22);
                Console.WriteLine(strMenuLines[1] + " : " + Presenter.getCurrentLevel());
                
                // Change position for sound's option
                CursorPosY = 20;
                string soundValue = (Presenter.getSoundModeActivated()) ? "ON" : "OFF";
                sentance = strMenuLines[0] + " : " + soundValue;
            }
            else
            {
                // Reset color for the previous line
                Console.SetCursorPosition(baseCursorPosX, CursorPosY);
                Console.WriteLine(Char.SPACE);

                Console.SetCursorPosition(baseCursorPosX + 5, 20);
                string currentSoundValue = (Presenter.getSoundModeActivated()) ? "ON" : "OFF";
                Console.WriteLine(strMenuLines[0] + " : " + currentSoundValue);

                // Change position for levels option
                CursorPosY = 22;
                sentance = strMenuLines[1] + " : " + Presenter.getCurrentLevel();
            }

            // Print cursor arrow character '>'
            Console.SetCursorPosition(baseCursorPosX, CursorPosY);
            ConsoleUtils.ClearCurrentConsoleLine();
            Console.WriteLine(Char.SELECT_CURSOR.ToString());

            // Print sentance for the current menu's selection
            Console.SetCursorPosition(baseCursorPosX + 5, CursorPosY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(sentance);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
