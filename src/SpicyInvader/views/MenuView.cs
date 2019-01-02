// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 17.12.2018

using SpicyInvader.presenters;
using System;
using System.Diagnostics;
using System.Threading;

namespace SpicyInvader.views
{
    class MenuView : View
    {
        public MenuPresenter Presenter { get; set; }   // DIP - Reference of the Presenter

        // Variable (proper to the view)
        private int xPosSelection;            // Y position of the Console cursor
        private int yPosSelection;            // X position of the Console cursor
        private int currentMenuCursorPos;     // Current position of the cursor on the menu
        private int maxMenuLength;        // Number of items in the menu
        private Screen[] screenName;                    // Name's list of screens that can be displayed
        

        private const int BASE_POSY_CURSOR = 20;


        /// <summary>
        /// Creation of the View object
        /// </summary>
        public override void onCreate(ScreenInfo screenInfo)
        {
            base.onCreate(screenInfo);

            // Show the header
            foreach(string line in Presenter.GetHeader())
            {
                int xPos = (width / 2) - (line.Length / 2);
                Console.SetCursorPosition(xPos, Console.CursorTop);
                Console.WriteLine(line);
            }

            // Show the body content
            int maxLineSize = (Character.SELECT_CURSOR + 5 + Screen.HIGHSCORES.ToString()).Length;
            yPosSelection = BASE_POSY_CURSOR;
            xPosSelection = (width / 2) - (maxLineSize / 2);
            
            screenName = new Screen[]{ Screen.PLAY, Screen.OPTIONS, Screen.HIGHSCORES, Screen.ABOUT };
            maxMenuLength = screenName.Length;
            currentMenuCursorPos = 0;

            foreach (Screen name in screenName)
            {
                Console.SetCursorPosition(xPosSelection, yPosSelection);
                Console.WriteLine(name.ToString());
                yPosSelection += 2;
            }
            

            // Show the footer
            // Todo : display a footer ? (credits) ?
        }

        /// <summary>
        /// The view is displayed on the screen and ready to be manipulated.
        /// </summary>
        public override void onResume()
        {
            base.onResume();

            // Change the position of the menu's cursor
            displayCursor(0, currentMenuCursorPos);

            // Keyboard event management
            keyboardEventHandler();
        }

        public override void onPause()
        {
            base.onPause();

            // Kills the keyboard press event's handler
            // Todo : eventThread.Join();
        }

        /// <summary>
        /// Display a 
        /// </summary>
        /// <param name="menuPosition"></param>
        private void displayCursor(int previousMenuPosition, int menuPosition)
        {
            // get current cursor position
            int currentConsoleCursorPos = Console.CursorLeft;

            // Erase the last caracter at the current menu's cursor position
            int oldYPos = BASE_POSY_CURSOR + (previousMenuPosition * 2);
            Console.SetCursorPosition(xPosSelection - 5, oldYPos);
            Console.Write(Character.SPACE);

            // change position of "menu's cursor selection"
            int nextYPos = BASE_POSY_CURSOR +  (menuPosition * 2);
            Console.SetCursorPosition(xPosSelection - 5, nextYPos);
            Console.Write(Character.SELECT_CURSOR);

            // reset default cursor position
            Console.SetCursorPosition(xPosSelection, currentConsoleCursorPos);
        }


        /// <summary>
        /// Handle keyboard press event safely
        /// </summary>
        private void keyboardEventHandler()
        {
            if(eventThread == null || eventThread.ThreadState == System.Threading.ThreadState.Stopped)
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
                                    if (currentMenuCursorPos - 1 >= 0)
                                    {
                                        currentMenuCursorPos--;
                                    }
                                    displayCursor(currentMenuCursorPos + 1, currentMenuCursorPos);
                                    break;

                                case ConsoleKey.DownArrow:
                                    if (currentMenuCursorPos + 1 < maxMenuLength)
                                    {
                                        currentMenuCursorPos++;
                                    }
                                    displayCursor(currentMenuCursorPos - 1, currentMenuCursorPos);
                                    break;

                                case ConsoleKey.Enter:
                                    Program.Navigate(getViewFrom(currentMenuCursorPos));
                                    break;

                                default:
                                    // Key press event not handleds
                                    break;
                            }
                        }

                    }
                });
            }

            // Start the Thread
            if (!eventThread.IsAlive)
                eventThread.Start();
        }

        public View getViewFrom(int position)
        {
            switch(position)
            {
                // PLAY
                case 0:
                    return new PlayView();

                // OPTION
                case 1:
                    return new OptionsView();

                // HIGHSCORES
                case 2:
                    return new HighscoresView();
                
                // ABOUT
                case 3:
                    return new AboutView();

                default:
                    throw new NotImplementedException("View's ID not handled.");
            }
        }

        public override void onDestroy()
        {
            Debug.WriteLine("Terminate  the ...");
        }
    }
}
