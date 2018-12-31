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

        // Variable

        private const int BASE_POSY_CURSOR = 20;

        private int xPosSelection;
        private int yPosSelection;
        private int currentMenuCursorPos;     // Current position of the cursor on the menu
        private int maxMenuLength;        // Number of items in the menu

        Screen[] screenName;                    // Name's list of screens that can be displayed

        public MenuView()
        {

        }

        public override void Display(ScreenInfo screenInfo)
        {
            base.Display(screenInfo);

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

            displayCursor(0, currentMenuCursorPos);

            // Show the footer

            // Keyboard event management
            keyboardHandler();
        }

        /// <summary>
        /// Handle keyboard press event safely
        /// </summary>
        private void keyboardHandler()
        {
            // Create a new Thread
            new Thread(() =>
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        switch (key.Key)
                        {
                            case ConsoleKey.UpArrow:
                                if(currentMenuCursorPos - 1 >= 0)
                                {
                                    currentMenuCursorPos--;
                                }
                                displayCursor(currentMenuCursorPos + 1, currentMenuCursorPos);
                                break;

                            case ConsoleKey.DownArrow:
                                if(currentMenuCursorPos + 1 < maxMenuLength)
                                {
                                    currentMenuCursorPos++;
                                }
                                displayCursor(currentMenuCursorPos - 1, currentMenuCursorPos);
                                break;

                            case ConsoleKey.Enter:
                                Program.Finish(this);
                                Debug.WriteLine("enter : Navigate to Screen X ...");
                                break;

                            default:
                                Debug.WriteLine("Not handled");
                                break;
                        }
                    }

                }
            }).Start();
            
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

        public override void Exit()
        {
            Debug.WriteLine("Terminate  the ...");
            //throw new NotImplementedException();
        }
    }
}
