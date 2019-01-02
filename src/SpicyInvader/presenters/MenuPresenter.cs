// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 17.12.2018

using SpicyInvader.models;
using SpicyInvader.presenters;
using SpicyInvader.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.presenters
{
    class MenuPresenter : Presenter
    {

        private MenuView View;          // View attached to the Presenter
        private MenuModel Model;        // Model attached to the Presenter

        public MenuPresenter(View view, Model model)
        {
            this.View = (MenuView) view;
            this.Model = (MenuModel) model;

            this.View.Presenter = this;
            this.Model.Presenter = this;
        }

        /// <summary>
        /// Return the header of the model
        /// </summary>
        /// <returns></returns>
        public string[] GetHeader()
        {
            return Model.GetHeader();
        }
    }
}
