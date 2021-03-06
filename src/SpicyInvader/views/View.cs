﻿// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 17.12.2018

using SpicyInvader.views.configs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

        private int width;      // Width of the View
        private int height;     // Height of the View

        protected int Width
        {
            get { return width; }

            set { width = value;  resizeWindow(); }
        }
        protected int Height
        {
            get { return height; }

            set { height = value;  resizeWindow(); }
        }

        public ScreenInfo ScreenInfo { get; set; }
        public LifecycleState State { get; set;  }
        protected Thread eventThread;             // Thread that process all instructions relative to an event

        public View() {

            // Init value of width
            width = Program.Width;
            height = Program.Height;


            // set the size of the window
            Width = 50;
            Height = 50;
        }

        /// <summary>
        /// Resize the size of the Window
        /// </summary>
        private void resizeWindow()
        {
            Program.Width = width;
            Program.Height = height;

            // modify the size of the window
            Console.SetWindowSize(Program.Width, Program.Height);

            // remove the scrollbar in the window
            Console.SetBufferSize(Program.Width, Program.Height);
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

            this.onCreate(ScreenInfo);
            this.onStart();
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
