// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2019

using SpicyInvader.presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.models
{

    /// <summary>
    /// Model that store all the data of the game.
    /// </summary>
    class PlayModel : Model
    {
        public Presenter Presenter { get; set; }    // Reference of the Presenter

        // Temporary variable for storing the current data of the game
        public int Score { get; set; }                          // The current score of the player
        public int Lives { get; set; }                          // The current number of lives of the player

        public PlayModel()
        {
            Score = 0;
            Lives = 3;  // Todo : Throw an Exception, if settings value > 3
        }
    }
}
