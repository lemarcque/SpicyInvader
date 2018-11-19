// Author : Henoc Sese
// Description : Manage all the logic part of the application (the game)
// Date : 12.11.2018

using SpicyInvaders.game;
using SpicyInvaders.game.character;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace SpicyInvaders
{
    /// <summary>
    /// Todo : Add a Controller object
    /// </summary>
    class Game : Environment
    {
        // Variable
        private Ship ship;                          // The ship of the player
        private List<Invader> ennemies;             // The list of invaders
        private List<Missile> currentMissiles;      // The missile that are currently displayed
        private int score;                          // The current score of the player
        private int lives;                          // The current number of lives of the player

        public static int MAX_INVADER_MOVE;     // The maximum number of times invaders can move, is the screenwidth - width of invaders'bloc size
        public const short LIVES = 3;               // The maximum lives of the player

        private const int INTERVAL_INVADER_MISSILE_SHOOT = 3000;

        /// <summary>
        /// Constructor
        /// </summary> of the class
        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;

            // configuration du jeu
            ship = new Ship();
            ship.setEnvironment(this);
            currentMissiles = new List<Missile>();

            // creation of ennemies
            createEnnemies();

            // creation of a timer
            Timer timer = new Timer(INTERVAL_INVADER_MISSILE_SHOOT);
            timer.Elapsed += new ElapsedEventHandler(onGenerateMissile);
            timer.Start();
        }

        /// <summary>
        /// Generate a new missile from the space invader at the same y position of the ship
        /// </summary>
        /// <param name=""></param>
        private void onGenerateMissile(object source, ElapsedEventArgs e)
        {
            const int SHOOTING_RANGE = 0;                   // The field of view of the invader to make a shoot
            foreach(Invader invader in ennemies)
            {
                if(invader.getX() >=  ship.getX() - SHOOTING_RANGE && invader.getX() <= ship.getX() + SHOOTING_RANGE)
                {
                    invader.shoot(true);
                    currentMissiles.Add(invader.GetMissile());
                }
            }
        }

        /// <summary>
        /// Creation of ennemies that will be displayed in a range of n ennemies
        /// </summary>
        public void createEnnemies()
        {
            ennemies = new List<Invader>();

            const short SPACE_BETWEEN_INVADER = 3;                // the space that is between the ennemies displayed
            const short nRow = 5;                                 // The number of row of invaders       (default : 5)                          
            const short nColumn = 10;                             // The number of column of invaders    (default : 10)
            MAX_INVADER_MOVE = width - (nColumn * SPACE_BETWEEN_INVADER);

            int basePositionX = 1;//((width / 2) - (nRow + SPACE_BETWEEN_INVADER)); // position  horizontale de base du bloc d'invaders default : 1 or 2
            int basePositionY = 8;                                                  // position verticale de base du bloc d'invaders    default : 8

            for (int x = 0; x < nColumn; x++)
            {
                for (int y = 0; y < nRow; y++)
                {
                    Crab crab = new Crab();
                    crab.setX(basePositionX + (SPACE_BETWEEN_INVADER * x));
                    crab.setY(basePositionY + (SPACE_BETWEEN_INVADER * y));
                    crab.setEnvironment(this);  // temp
                    ennemies.Add(crab);
                }
            }
        }

        /// <summary>
        /// Return the list of ennemies that are currently displayed
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

        public int getLives()
        {
            return lives;
        }

        public List<Missile> getCurrentMissiles()
        {
            return currentMissiles;
        }

        /// <summary>
        /// Terminate the game
        /// </summary>
        public void finish()
        {
            ship.setLife(false);
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
