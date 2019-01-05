// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2019

using SpicyInvader.models;
using SpicyInvader.views;
using SpicyInvader.views.utils;
using SpicyInvaders;
using SpicyInvaders.domain.character;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.presenters
{
    class PlayPresenter : Presenter
    {

        private PlayView View;          // View attached to the Presenter
        private PlayModel Model;        // Model attached to the Presenter

        public PlayPresenter(View view, Model model)
        {
            this.View = (PlayView) view;
            this.Model = (PlayModel) model;

            this.View.Presenter = this;
            this.Model.Presenter = this;

            // Init the View
            Initialization();
        }

        private void Initialization()
        {
            View.ShowLives(Model.Lives);
            View.ShowScore(Model.Score);

            // Display the Ship
            View.ShowShip();

            // Creation of ennemies
            this.CreateInvaders();

            // Display all Invaders
            View.ShowInvaders();

            // Start a loop ??
            // creation of a timer for shooting missile for invaders)
            /*timer = new Timer(INTERVAL_INVADER_MISSILE_SHOOT);
            timer.Elapsed += new ElapsedEventHandler(OnGenerateMissile);
            timer.Start();*/

            // Start a keyboard handler
        }

        public int getCurrentScore()
        {
            return Model.Score;
        }

        public int getCurrentLives()
        {
            return Model.Lives;
        }

        /// <summary>
        /// Return the Ship of the player
        /// </summary>
        /// <returns></returns>
        public Ship GetShip()
        {
            return Model.Ship;
        }

        /// <summary>
        /// Return the List of Invaders
        /// </summary>
        /// <returns></returns>
        public List<Invader> GetInvaders()
        {
            return Model.Invaders;
        }

        /// <summary>
        /// Creation of Invader's object
        /// They will be displayed in a range of n ennemies
        /// </summary>
        private void CreateInvaders()
        {

            int maxInvaderMove;     // The maximum number of times invaders can move, is the screenwidth - width of invaders'bloc size
            int row = 6;                                                  // The number of row of invaders       (default : 5)                     
            int column = 10;                                              // The number of column of invaders    (default : 10)
            //int currentCloserRow = nRow;                                        // The row which ship are closer
            const short SPACE_BETWEEN_INVADER = 3;                          // the space that is between the ennemies displayed
            maxInvaderMove = Program.Width - (column * SPACE_BETWEEN_INVADER);

            int basePositionX = 1;//((width / 2) - (nRow + SPACE_BETWEEN_INVADER)); // position  horizontale de base du bloc d'invaders default : 1 or 2
            int basePositionY = 8;                                                  // position verticale de base du bloc d'invaders    default : 8

            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    // Create new Crab (Invader object)
                    Crab crab = new Crab();
                    crab.SetX(basePositionX + (SPACE_BETWEEN_INVADER * y));
                    crab.SetY(basePositionY + (SPACE_BETWEEN_INVADER * x));
                    crab.Row = x + 1;
                    crab.Column = y + 1;
                    Model.Invaders.Add(crab);
                }
            }
        }

        /// <summary>
        /// Move the player's Ship
        /// </summary>
        /// <param name="direction"></param>
        public void moveShip(Direction direction)
        {
            switch(direction)
            {
                case Direction.Left:
                    if (Model.Ship.X - Model.Ship.SpeedX > 0)
                    {
                        ConsoleUtils.RemoveChar(Model.Ship.X, Model.Ship.Y);
                        Model.Ship.X -= Model.Ship.SpeedX;
                    }
                    
                    View.ShowShip();
                    break;

                case Direction.Right:
                    if (Model.Ship.X + Model.Ship.SpeedX < Program.Width)
                    {
                        ConsoleUtils.RemoveChar(Model.Ship.X, Model.Ship.Y);
                        Model.Ship.X += Model.Ship.SpeedX;
                    }
                    
                    View.ShowShip();
                    break;
            }
        }
    }
}
