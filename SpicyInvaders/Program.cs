using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    class Program
    {

        // Components application
        Interface interfaces;

        

        static void Main(string[] args)
        {
            
            new Program();
        }

        public Program()
        {
            interfaces = new Interface(50, 50);   // Initialisation de l'interface
        } 
        
    }
}
