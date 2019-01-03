// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 02.01.2019

using SpicyInvader.models;
using SpicyInvader.views;
using System;

namespace SpicyInvader.presenters
{
    class OptionsPresenter : Presenter
    {

        private OptionsView View;          // View attached to the Presenter
        private OptionsModel Model;        // Model attached to the Presenter

        public OptionsPresenter(View view, Model model)
        {
            this.View = (OptionsView) view;
            this.Model = (OptionsModel) model;

            this.View.Presenter = this;
            this.Model.Presenter = this;
        }

        /// <summary>
        /// Control and pass the data to the "repository" object (Model)
        /// </summary>
        /// <param name="soundMode"></param>
        /// <param name="level"></param>
        public void updateGameOptions(String soundMode, Level level)
        {
            Model.updateGameOptions(soundMode, level);
        }

        /// <summary>
        /// Return the current sound's mode
        /// </summary>
        /// <returns></returns>
        public bool getSoundModeActivated()
        {
            return Model.IsSoundActivated;
        }

        /// <summary>
        /// Return the current level of the game
        /// </summary>
        /// <returns></returns>
        public Level getCurrentLevel()
        {
            return Model.CurrentLevel;
        }

        /// <summary>
        /// Switch between sound's mdoe
        /// </summary>
        public void switchSoundMode()
        {
            Model.IsSoundActivated = !Model.IsSoundActivated;
        }

        /// <summary>
        /// Switch the between level
        /// </summary>
        public void switchCurrentLevel()
        {
            Model.CurrentLevel = (Model.CurrentLevel == Level.JEDI) ? Level.PADAWAN : Level.JEDI; ;
        }
    }
}
