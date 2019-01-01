// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 17.12.2018

using SpicyInvader.data;
using SpicyInvader.presenters;
using SpicyInvader.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.models
{
    /// <summary>
    /// 
    /// </summary>
    class MenuModel : Model
    {
        public Presenter Presenter { get; set; }    // DIP - Reference of the Presenter
        private Dictionary<Screen, String> menu;

        public MenuModel()
        {
            // TODO : Implement Dictionary for menu
        }

        public string[] GetHeader()
        {
            return TextFileManager.GetDataFrom(@"../../data/txt/spaceinvader-6.txt");
        }
    }
}
