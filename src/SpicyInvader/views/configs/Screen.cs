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
    /// Define a list of application's screen
    /// </summary>
    public enum Screen
    {
        MENU,
       
        PLAY,   // GAMEPLAY
        GAMEOVER,

        OPTIONS,
        HIGHSCORES,
        CREDITS,
        ABOUT,
        QUIT        // EXIT
    }
}
