// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2019

using SpicyInvader.domain;
using SpicyInvader.models;
using SpicyInvader.presenters.listeners;
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
    class PlayPresenter : Presenter, ShootListener
    {

        private PlayView View;          // View attached to the Presenter
        private PlayModel Model;        // Model attached to the Presenter

        // Business Logic
        private Engine engine;

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

            // Initialiation of the Businss Logic's object
            engine = new Engine(Model, this);

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
        /// Return the invader that has shooting the last missile.
        /// </summary>
        /// <returns></returns>
        public Invader getCurrentInvaderMissileOwner()
        {
            return Model.CurrentInvaderMissileOwner;
        }

        /// <summary>
        /// Creation of Invader's object
        /// They will be displayed in a range of n ennemies
        /// </summary>
        private void CreateInvaders()
        {

            int row = Engine.MAX_ROW_INVADERS;                                                  // The number of row of invaders       (default : 6)                     
            int column = Engine.MAX_COLUMN_INVADERS;                                              // The number of column of invaders    (default : 10)
            engine.CurrentCloserRow = row;                                        // The row which ship are closer

            Engine.MAX_INVADER_MOVE = Program.Width - (column * Engine.SPACE_BETWEEN_INVADER);

            int basePositionX = 1;//((width / 2) - (nRow + SPACE_BETWEEN_INVADER)); // position  horizontale de base du bloc d'invaders default : 1 or 2
            int basePositionY = 8;                                                  // position verticale de base du bloc d'invaders    default : 8

            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    // Create new Crab (Invader object)
                    Crab crab = new Crab();
                    crab.SetX(basePositionX + (Engine.SPACE_BETWEEN_INVADER * y));
                    crab.SetY(basePositionY + (Engine.SPACE_BETWEEN_INVADER * x));
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

        /// <summary>
        /// Callback to prevent that an invader has shoot
        /// </summary>
        /// <param name="invader"></param>
        public void OnShoot()
        {
            int posX = Model.CurrentInvaderMissileOwner.GetMissile().X;
            int posY = Model.CurrentInvaderMissileOwner.GetMissile().Y;
            View.MoveInvaderMissile(posX, posY);
        }

        public void TempRemoveMissile()
        {
            View.TempRemoveMissile();
        }

        public void UpdateLives()
        {
            View.UpdateMenu();
        }
    }
}
