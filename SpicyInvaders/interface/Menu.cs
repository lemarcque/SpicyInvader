using System;
using System.Diagnostics;

namespace SpicyInvaders.game
{
    class Menu
    {

        private static int MARGIN = 5;

        // Position sur l'interface du bloc de score
        private static int POS_SCORE_X = MARGIN;
        private static int POS_SCORE_Y = 2;

        // Position sur l'interface du bloc de vie
        private static int POS_LIVES_X = 0;
        private static int POS_LIVES_Y = 2;

        private int width;      // width of the menu
        private int height;     // height of the menu

        public Menu(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Change the score in the menu's interface
        /// </summary>
        /// <param name="score"> the current score of the game</param>
        public void setScore(int score)
        {
            const String sentanceScore = "Score ";
            Console.SetCursorPosition(POS_SCORE_X, POS_SCORE_Y);

            // change the color
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(sentanceScore);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(score);
        }

        public void setLives(int lives)
        {
            const String sentanceLives = "Lives";
            String sentanceHealth = "";

            for (int i = 0; i < lives; i++)
            {
                sentanceHealth += " ■";
            }

            // Show the sentance
            Console.SetCursorPosition(width - (MARGIN) - sentanceLives.Length - sentanceHealth.Length, POS_LIVES_Y);
            Console.Write(sentanceLives);

            // Show the health
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(sentanceHealth);

        }
    }
}
