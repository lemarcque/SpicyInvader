// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2019

using SpicyInvader.models;
using SpicyInvader.views;
using SpicyInvaders.domain.character;
using System;
using System.Collections.Generic;
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
            View.showLives(Model.Lives);
            View.showScore(Model.Score);

            // Display the Ship
            
            View.ShowShip();

            // creation of ennemies

            // Display all Invaders

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
    }
}
