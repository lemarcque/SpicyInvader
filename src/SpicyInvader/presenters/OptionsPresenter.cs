// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 02.01.2019

using SpicyInvader.models;
using SpicyInvader.views;

namespace SpicyInvader.presenters
{
    class OptionsPresenter : Presenter
    {

        private OptionsView View;          // View attached to the Presenter
        private OptionsModel Model;        // Model attached to the Presenter

        public MenuPresenter(View view, Model model)
        {
            this.View = (OptionsView) view;
            //this.Model = (OptionsModel) model;

            //this.View.Presenter = this;
            //this.Model.Presenter = this;
        }

    }
}
