﻿// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 17.12.2018

using SpicyInvader.models;
using SpicyInvader.presenters;
using SpicyInvader.views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader
{

    /// <summary>
    /// Class main of the program.
    /// </summary>
    class Program
    {
        private static bool isRunning;                      // Indicates if the program is running
        private static List<ScreenInfo> navigationList;     // The list of screen that are currently displayed
        private static byte counterNavigationId;            // The counter to generate screen's navigation id
        private static byte Width;                          // Width size (in character) of the program
        private static byte Height;                         // Height size (in character) of the program

        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Arguments from the console inpu</param>
        static void Main(string[] args)
        {
            Program.Init();
            Program.Start();
        }

        /// <summary>
        /// Initialization of the program.
        /// </summary>
        public static void Init()
        {
            navigationList = new List<ScreenInfo>();
        }

        public static void Start()
        {

            if(!isRunning) {
                // Creation of Dependency
                MenuView view = new MenuView();
                MenuModel model = new MenuModel();
                MenuPresenter presenter = new MenuPresenter(view, model);
                Navigate(view);

                // Enable the running mode
                isRunning = true;
            }

            // Stop the program, temporarily
            //Console.Read();
            Console.CursorVisible = false;
        }

        private static void Navigate(View view)
        {
            // TODO : state of the current screen are "snapshoted"
            // and stored in In-memory cache.
            

            // Navigate to the view specified
            view.Display(new ScreenInfo(
                view.GetHashCode(),
                view.GetType().Name));

            // Save the view to the list of views currently displayed
            navigationList.Add(view.ScreenInfo);
        }

        public static void Finish(View view)
        {
            view.Exit();
        }
    }
}
