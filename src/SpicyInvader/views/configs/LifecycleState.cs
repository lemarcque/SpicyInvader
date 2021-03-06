﻿// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 31.12.2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.views.configs
{

    /// <summary>
    /// Define in which state a View is.
    /// </summary>
    public enum LifecycleState
    {
        CREATE,
        START,
        RESTART,
        RESUME,
        PAUSE,
        STOP,
        DESTROY
    }
}
