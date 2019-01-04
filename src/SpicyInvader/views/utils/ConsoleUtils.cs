// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 02.01.2019

using System;

namespace SpicyInvader.views.utils
{
    /// <summary>
    /// Class that add more options for Console output management
    /// </summary>
    class ConsoleUtils
    {

        /// <summary>
        /// Clear all a specific lines instead of whole the console content.
        /// </summary>
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            int currentColumnCursor = Console.CursorLeft;

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));

            Console.SetCursorPosition(currentColumnCursor, currentLineCursor);
            //Console.SetCursorPosition(0, currentLineCursor);
        }

        /// <summary>
        /// Return the current position of the Consle cursor to perform an operation
        /// </summary>
        /// <returns></returns>
        public static CursorPosition SnapCursorPosition()
        {
            return new CursorPosition(Console.CursorTop, Console.CursorLeft);
        }

        /// <summary>
        /// Reset the position of the console's cursor
        /// </summary>
        /// <param name="position"></param>
        public static void ResetCursorPosition(CursorPosition position)
        {
            Console.SetCursorPosition(position.Left, position.Top);
        }
    }
}
