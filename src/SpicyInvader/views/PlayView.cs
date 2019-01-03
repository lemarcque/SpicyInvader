// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 31.12.2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.views
{
    class PlayView : View
    {


        public override void onCreate(ScreenInfo screenInfo)
        {
            base.onCreate(screenInfo);

            // ...
        }

        /// <summary>
        /// The View lose its focus and enter in to the paused state.
        /// </summary>
        public override void onPause()
        {
            base.onPause();

            // Kills the keyboard press event's handler
            // Todo : eventThread.Join();
        }
    }
}
