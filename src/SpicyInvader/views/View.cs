// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 17.12.2018

using SpicyInvader.views.configs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.views
{

    /// <summary>
    /// Define a View (MVP)
    /// The structure of the class and how the object works contains some charateristic
    /// of 'Activity' object from the Android frameworks
    /// </summary>
    public abstract class View
    {

        protected byte width { get; }
        protected byte height { get; }
        public ScreenInfo ScreenInfo { get; set; }
        public LifecycleState State { get; set;  } 

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
        /// Creation of the View object
        /// </summary>
        public virtual void onCreate(ScreenInfo screenInfo)
        {
            State = LifecycleState.CREATE;

            Debug.WriteLine("Creating the view : " + screenInfo.Name);

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
        /// Prepare the components and data associated with to be displayed.
        /// </summary>
        public void onStart()
        {
            State = LifecycleState.START;

            this.onResume();
        }

        /// <summary>
        /// The view is displayed on the screen and ready to be manipulated.
        /// </summary>
        public virtual void onResume()
        {
            State = LifecycleState.RESUME;
        }

        /// <summary>
        /// The View lose its focus and enter in to the paused state.
        /// </summary>
        public virtual void onPause()
        {
            State = LifecycleState.PAUSE;

            // Erase all graphical element on screen
            Console.Clear();
        }

        /// <summary>
        ///  The View is no longer visible to the user because either
        ///  new View gets started or existing View gets resumed state.
        /// </summary>
        public virtual void onStop()
        {
            State = LifecycleState.STOP;
        }

        /// <summary>
        /// The stopped View gets restarted.
        /// Restores the state of the activity from the time that it was stopped.
        /// </summary>
        public virtual void onRestart()
        {
            State = LifecycleState.RESTART;
        }


        /// <summary>
        /// The system calls this callback before destroy the View.
        /// Terminate the displaying of the View on screen.
        /// </summary>
        public virtual void onDestroy()
        {
            State = LifecycleState.DESTROY;
        }
    }
}
