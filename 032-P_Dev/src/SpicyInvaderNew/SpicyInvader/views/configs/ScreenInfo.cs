// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 17.12.2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.views
{

    /// <summary>
    /// Give information about screen display on the screen
    /// </summary>
    public class ScreenInfo
    {
        private int Id { get; }         // unique identifier associate at each views displayed on screen
        private string Name { get; }    // The name of the view displayed on screen

        public ScreenInfo(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
