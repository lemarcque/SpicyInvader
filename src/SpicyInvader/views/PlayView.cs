// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 31.12.2018

using SpicyInvader.presenters;
using SpicyInvader.views.utils;
using SpicyInvaders;
using SpicyInvaders.domain.character;
using System;
using System.Diagnostics;
using System.Threading;

namespace SpicyInvader.views
{

    /// <summary>
    /// Display the game.
    /// </summary>
    class PlayView : View
    {


        public PlayPresenter Presenter { get; set; }    // Reference of the Presenter

        // Margin of the screen
        private static int MARGIN = 5;

        // Position sur l'interface du bloc de score
        private static int POS_SCORE_X = MARGIN;
        private static int POS_SCORE_Y = 2;

        // Position sur l'interface du bloc de vie
        private static int POS_LIVES_X = 0;
        private static int POS_LIVES_Y = 2;


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
                int cond = 2000;
                int thick = 0;

                // Create a new Thread
                eventThread = new Thread(() =>
                {
                    while (State == configs.LifecycleState.RESUME)
                    {
                        while(thick % cond == 0)
                        {
                            if (Console.KeyAvailable)
                            {
                                ConsoleKeyInfo key = Console.ReadKey(true);

                                switch (key.Key)
                                {
                                    case ConsoleKey.LeftArrow:
                                         Presenter.moveShip(Direction.Left);
                                        break;

                                    case ConsoleKey.RightArrow:
                                        Presenter.moveShip(Direction.Right);
                                        break;

                                    case ConsoleKey.Spacebar:
                                        // Todo : ...

                                        break;

                                    case ConsoleKey.Escape:
                                        Program.Finish(this);
                                        break;
                                }
                            }
                        }

                        thick++;
                    }
                });
            }

            // Start the thread
            eventThread.Start();
        }

        /// <summary>
        /// Change the score in the menu's interface
        /// </summary>
        /// <param name="score"> the current score of the game</param>
        public void ShowScore(int score)
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

        /// <summary>
        /// Change the score in the menu's interface
        /// </summary>
        /// <param name="lives"></param>
        public void ShowLives(int lives)
        {
            const String sentanceLives = "Lives";
            String sentanceHealth = "";
            const int sentanceLongestSize = 6;


            for (int i = 0; i < lives; i++)
            {
                sentanceHealth += " ■";
            }

            
            // Show the sentance "Lives"
            Console.SetCursorPosition(Width - (MARGIN) - sentanceLongestSize - sentanceLongestSize, POS_LIVES_Y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sentanceLives);

            // Show the health
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(sentanceHealth);

        }

        public void UpdateMenu()
        {
            // Reset the lines
            Console.SetCursorPosition(0, POS_LIVES_Y);
            ConsoleUtils.ClearCurrentConsoleLine();

            // Display data
            ShowScore(Presenter.getCurrentScore());
            ShowLives(Presenter.getCurrentLives());
        }

        /// <summary>
        /// Display the ship of the player
        /// </summary>
        public void ShowShip()
        {

            // Draw the ship
            int posX = Presenter.GetShip().GetX();
            int posY = Presenter.GetShip().GetY();
            ConsoleUtils.FastDraw(posX, posY, Presenter.GetShip());
        }

        /// <summary>
        /// Display all character of the invaders
        /// </summary>
        public void ShowInvaders()
        {
            foreach (Invader invader in Presenter.GetInvaders())
            {
                // Draw the Ship
                ConsoleUtils.FastDraw(invader.X, invader.Y, invader);
            }
        }

        public void MoveInvaderMissile(int posX, int posY)
        {
            // Draw the Missile
            Missile missile = Presenter.getCurrentInvaderMissileOwner().GetMissile();
            ConsoleUtils.FastDraw(posX, posY, missile);
        }

        public void TempRemoveMissile()
        {
            int posX = Presenter.getCurrentInvaderMissileOwner().GetMissile().X;
            int posY = Presenter.getCurrentInvaderMissileOwner().GetMissile().Y;
            ConsoleUtils.RemoveChar(posX, posY);
        }
    }
}
