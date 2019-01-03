// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 31.12.2018

using SpicyInvader.presenters;
using System;

namespace SpicyInvader.views
{

    /// <summary>
    /// Display the game.
    /// </summary>
    class PlayView : View
    {


        public Presenter Presenter { get; set; }    // Reference of the Presenter

        // Margin of the screen
        private static int MARGIN = 5;

        // Position sur l'interface du bloc de score
        private static int POS_SCORE_X = MARGIN;
        private static int POS_SCORE_Y = 2;

        // Position sur l'interface du bloc de vie
        private static int POS_LIVES_X = 0;
        private static int POS_LIVES_Y = 2;

        public override void onCreate(ScreenInfo screenInfo)
        {
            base.onCreate(screenInfo);

            // Temp : testing

            Console.Read();
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
        /// Change the score in the menu's interface
        /// </summary>
        /// <param name="score"> the current score of the game</param>
        public void showScore(int score)
        {
            const String sentanceScore = "Score ";
            Console.SetCursorPosition(POS_SCORE_X, POS_SCORE_Y);

            // Show the sentance "Score"
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sentanceScore);

            // Sow the current score
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(score);
        }

        public void showLives(int lives)
        {
            const String sentanceLives = "Lives";
            String sentanceHealth = "";
            const int sentanceLongestSize = 6;

            for (int i = 0; i < lives; i++)
            {
                sentanceHealth += " ■";
            }

            // Show the sentance "Lives"
            Console.SetCursorPosition(width - (MARGIN) - sentanceLongestSize - sentanceLongestSize, POS_LIVES_Y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sentanceLives);

            // Show the health
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(sentanceHealth);

        }
    }
}
