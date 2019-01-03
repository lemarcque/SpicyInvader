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

        public bool getSoundModeActivated()
        {
            return Model.IsSoundActivated;
        }

        public Level getCurrentLevel()
        {
            return Model.CurrentLevel;
        }

        public void switchSoundMode()
        {
            Model.IsSoundActivated = !Model.IsSoundActivated;
        }

        public void switchCurrentLevel()
        {
            Model.CurrentLevel = (Model.CurrentLevel == Level.JEDI) ? Level.PADAWAN : Level.JEDI; ;
        }
    }
}
