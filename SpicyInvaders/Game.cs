using SpicyInvaders.game;
using SpicyInvaders.game.character;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    /// <summary>
    /// Todo : Add a Controller object
    /// </summary>
    class Game : Environment
    {
        // Variable
        Ship ship;                          // The ship of the player
        List<Invader> ennemies;             // The list of ennemies
        List<Missile> currentMissiles;      // The missile that are currently displayed
        int score;                          // The current score of the player
        int lives;                          // The current number of lives of the player

        /// <summary>
        /// Constructor
        /// </summary> of the class
        public Game(int width, int height)
        {
            Debug.WriteLine(width);
            Debug.WriteLine(width);

            this.width = width;
            this.height = height;

            // configuration du jeu
            ship = new Ship();
            ship.setEnvironment(this);
            currentMissiles = new List<Missile>();

            // creation of ennemies
            createEnnemies();
        }

        /// <summary>
        /// Creation of ennemies that will be displayed in a range of n ennemies
        /// </summary>
        public void createEnnemies()
        {
            ennemies = new List<Invader>();

            const int SPACE_BETWEEN_INVADER = 2;                // the space that is between the ennemies displayed
            const int nRow = 7;
            const int nColumn = 10;

            int basePositionX = (width / 2) - (1 * (nRow + SPACE_BETWEEN_INVADER));
            int basePositionY = 8;


            for (int x = 0; x < nColumn; x++)
            {
                for (int y = 0; y < nRow; y++)
                {
                    Crab crab = new Crab();
                    crab.setX(basePositionX + (SPACE_BETWEEN_INVADER * x));
                    crab.setY(basePositionY + (SPACE_BETWEEN_INVADER * y));
                    ennemies.Add(crab);
                }
            }
        }

        /// <summary>
        /// Return the list of ennmies that are currently displayed
        /// </summary>
        /// <returns>Collection of Invaders object</returns>
        public List<Invader> getCurrentEnnemies()
        {
            return this.ennemies;
        }

        /// <summary>
        /// Return the ship object
        /// </summary>
        /// <returns>Ship object</returns>
        public Character getShip()
        {
            return ship;
        }

        // ----- action

        /// <summary>
        /// Handling the shoot's missile of the ship
        /// </summary>
        public void shoot()
        {
            if(!ship.getIsShooting())
            {
                ship.shoot(true);
                currentMissiles.Add(ship.GetMissile());
            }
        }

        public void dishoot()
        {
            ship.shoot(false);
            currentMissiles.Remove(ship.GetMissile());
           
        }

        public int getScore()
        {
            return score;
        }

        public void setScore(int scoreGain)
        {
            score += scoreGain;
        }

        public void setLives(int lives)
        {
            this.lives = lives;
        }

        public List<Missile> getCurrentMissiles()
        {
            return currentMissiles;
        }
    }

    abstract class Environment
    {
        protected int width;        // Limite de l'environnement du jeu en largeur
        protected int height;       // Limite de l'environnement du jeu en hauteur

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }
    }
}
