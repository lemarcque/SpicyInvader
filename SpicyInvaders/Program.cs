// Aut// Author : Henoc Sese
// Description : Space Invaders game
// Date : 19.11.2018
// Locality : Lausanne 

namespace SpicyInvaders
{

    /// <summary>
    /// Main class called when starting the application
    /// </summary>
    class Program
    {

        // Components application
        Interface interfaces;

        
        /// <summary>
        /// The first main method called at the start of the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {            new Program();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Program()
        {
            // Initialisation of the interface
            // width : 50
            // height : 50
            interfaces = new Interface(50, 50);
        } 
        
    }
}
