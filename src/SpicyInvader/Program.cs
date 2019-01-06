// Author : Henoc Sese
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
        private static List<View> navigationList;           // The list of screen that are currently displayed
        private static byte counterNavigationId;            // The counter to generate screen's navigation id
        public static int Width { get; set; }                          // Width size (in character) of the program
        public static int Height { get; set; }                         // Height size (in character) of the program

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
            navigationList = new List<View>();

            // Init value of the width and height console
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
        }

        public static void Start()
        {

            if(!isRunning) {
                // Creation of Dependency
                Navigate(new MenuView());

                // Enable the running mode
                isRunning = true;
            }

            // Stop the program, temporarily
            //Console.Read();
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Stop/Quit the program.
        /// </summary>
        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void Navigate(View view)
        {
            

            // TODO : state of the current screen are "snapshoted"
            // and stored in In-memory cache.

            if (navigationList.Count > 0)
                navigationList[navigationList.Count - 1].onPause();

            // Init presenter
            Presenter presenter = DependencyFactory.getDependency(view);

            // Navigate to the view specified
            view.onCreate(new ScreenInfo(
                view.GetHashCode(),
                view.GetType().Name));

            view.onStart();

            // Save the view to the list of views currently displayed
            navigationList.Add(view);
        }

        public static void Finish(View view)
        {
            // Remove the View from the screen
            view.onPause();
            view.onDestroy();

            // Remove the View from the list of current views displayed
            navigationList.Remove(view);

            // Display the previous View on screen
            if(navigationList.Count > 0)
            {
                navigationList[navigationList.Count - 1].onRestart();
            }else
            {
                Debug.WriteLine("Exit..");
                System.Environment.Exit(1);
            }
        }
    }
}
