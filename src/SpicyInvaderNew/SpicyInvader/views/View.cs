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
    /// Define a View (MVP)
    /// </summary>
    public abstract class View
    {

        protected byte width { get; }
        protected byte height { get; }
        public ScreenInfo ScreenInfo { get; set; }

        public View() {
        
            // set the size of the window
            width = 50;
            height = 50;

            // modify the size of the window
            // remove the scrollbar in the window
            Console.SetWindowSize(width, height);       
            Console.SetBufferSize(width, height);       
        }

        /// <summary>
        /// Called when the Program should display the View
        /// </summary>
        public virtual void Display(ScreenInfo screenInfo)
        {
            if (screenInfo != null)
            {
                this.ScreenInfo = screenInfo;
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Terminate the displaying of the View on screen.
        /// </summary>
        public abstract void Exit();
    }
}
