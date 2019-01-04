// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2018


// Author : Henoc Sese
// 
// Date : 12.11.2018

using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace SpicyInvaders
{
    /// <summary>
    /// Description : Manage all the logic part of the application (the game)
    /// </summary>
    class Game : Environment
    {
        // Variable
        private Ship ship;                          // The ship of the player
        private List<Invader> ennemies;             // The list of invaders
        private List<Missile> currentMissiles;      // The missile that are currently displayed
        private int score;                          // The current score of the player
        private int lives;                          // The current number of lives of the player

        // Relative at the ennemies
        private int nRow;
        private int nColumn;
        private int currentCloserRow;


        public static int MAX_INVADER_MOVE;     // The maximum number of times invaders can move, is the screenwidth - width of invaders'bloc size
        public const short LIVES = 3;               // The maximum lives of the player
        private const int INTERVAL_INVADER_MISSILE_SHOOT = 1000; // default : 3000
        private static bool invaderMissileMoving;   // Indicates if a ennemies missile is moving
        Timer timer;


        /// <summary>
        /// Constructor
        /// </summary> of the class
        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;

            // configuration du jeu
            ship = new Ship();
            ship.SetEnvironment(this);
            currentMissiles = new List<Missile>();

            // creation of ennemies
            CreateEnnemies();

            // creation of a timer
            timer = new Timer(INTERVAL_INVADER_MISSILE_SHOOT);
            timer.Elapsed += new ElapsedEventHandler(OnGenerateMissile);
            timer.Start();
        }

        /// <summary>
        /// Generate a new missile from the space invader at the same y position of the ship
        /// </summary>
        /// <param name=""></param>
        private void OnGenerateMissile(object source, ElapsedEventArgs e)
        {
            const int SHOOTING_RANGE = 0;                   // The field of view of the invader to make a shoot

            List<Invader> invaders = GetInvadersAtRow(GetCloserInvadersRow());

            foreach(Invader invader in invaders)
            {
                // verify that the invader is "alive"
                if(invader.IsAlive)
                {
                    // Verify that no other invader's missile are moving
                    if (!invaderMissileMoving)
                    {
                        if (invader.GetX() >= ship.GetX() - SHOOTING_RANGE && invader.GetX() <= ship.GetX() + SHOOTING_RANGE)
                        {
                            //invaderMissileMoving = true;
                            //invader.Shoot(true);
                            //currentMissiles.Add(invader.GetMissile());
                        }
                    }
                }
            }
        }



        /// <summary>
        /// Creation of ennemies that will be displayed in a range of n ennemies
        /// </summary>
        public void CreateEnnemies()
        {
            ennemies = new List<Invader>();
            
            this.nRow = 5;                                                  // The number of row of invaders       (default : 5)                     
            this.nColumn = 10;                                              // The number of column of invaders    (default : 10)
            currentCloserRow = nRow;                                        // The row which ship are closer

            const short SPACE_BETWEEN_INVADER = 3;                          // the space that is between the ennemies displayed
            MAX_INVADER_MOVE = width - (nColumn * SPACE_BETWEEN_INVADER);

            int basePositionX = 1;//((width / 2) - (nRow + SPACE_BETWEEN_INVADER)); // position  horizontale de base du bloc d'invaders default : 1 or 2
            int basePositionY = 8;                                                  // position verticale de base du bloc d'invaders    default : 8

            for (int x = 0; x < nRow; x++)
            {
                for (int y = 0; y < nColumn; y++)
                {
                    Crab crab = new Crab();
                    crab.SetX(basePositionX + (SPACE_BETWEEN_INVADER * y));
                    crab.SetY(basePositionY + (SPACE_BETWEEN_INVADER * x));
                    crab.Row = x + 1;
                    crab.Column = y + 1;
                    crab.SetEnvironment(this);  // temp
                    ennemies.Add(crab);
                }
            }
        }

        public int GetCloserInvadersRow()
        {
            for (int i = 0; i < nRow; i++)
            {
                List<Invader> invaders = GetInvadersAtRow(nRow);

                foreach (Invader invader in invaders)
                {
                    if (invader.IsAlive)
                    {
                        currentCloserRow = nRow;
                        return currentCloserRow;
                    }
                }
            }

            return currentCloserRow;
        }

        /// <summary>
        /// Return a list of invaders from a specific row
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private List<Invader> GetInvadersAtRow(int rowIndex)
        {
            List<Invader> invaders = new List<Invader>();

            foreach (Invader invader in ennemies)
            {
                if (invader.Row == rowIndex)
                {
                    invaders.Add(invader);
                }
            }


            return invaders;
        }

        /// <summary>
        /// Return the list of ennemies that are currently displayed
        /// </summary>
        /// <returns>Collection of Invaders object</returns>
        public List<Invader> GetCurrentEnnemies()
        {
            return this.ennemies;
        }

        /// <summary>
        /// Return the ship object
        /// </summary>
        /// <returns>Ship object</returns>
        public Character GetShip()
        {
            return ship;
        }

        // ----- action

        /// <summary>
        /// Handling the shoot's missile of the ship
        /// </summary>
        public void Shoot()
        {
            if(!ship.GetIsShooting)
            {
                ship.Shoot(true);
                currentMissiles.Add(ship.GetMissile());
            }
        }

        public void Dishoot()
        {
            ship.Shoot(false);
            currentMissiles.Remove(ship.GetMissile());
        }

        public void Dishoot(Missile missile)
        {
            foreach(Invader invader in ennemies)
            {
                if(invader.GetMissile() == missile)
                {
                    invaderMissileMoving = false;
                    invader.Shoot(false);
                    currentMissiles.Remove(missile);
                }
            }
        }

        /// <summary>
        /// Return the current game's score
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return score;
        }

        /// <summary>
        /// Update the value of the game's score
        /// </summary>
        /// <param name="scoreGain"></param>
        public void SetScore(int scoreGain)
        {
            score += scoreGain;
        }

        /// <summary>
        /// Update the number of lives
        /// </summary>
        /// <param name="lives"></param>
        public void SetLives(int lives)
        {
            this.lives = lives;
        }

        /// <summary>
        /// Return the number of "lives" that remains 
        /// </summary>
        /// <returns>The number of lives</returns>
        public int GetLives()
        {
            return lives;
        }

        /// <summary>
        /// Return the list of missile that a moving on the screen
        /// </summary>
        /// <returns>List of missiles</returns>
        public List<Missile> GetCurrentMissiles()
        {
            return currentMissiles;
        }

        /// <summary>
        /// Terminate the game
        /// </summary>
        public void Finish()
        {
            ship.SetLife(false);
        }
    }

    /// <summary>
    /// Environment is the representation of the space of the game in the screen
    /// </summary>
    abstract class Environment
    {
        protected int width;        // Limite de l'environnement du jeu en largeur
        protected int height;       // Limite de l'environnement du jeu en hauteur

        /// <summary>
        /// Return the width of the environment
        /// </summary>
        /// <returns>width size</returns>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// Return the height of the environement
        /// </summary>
        /// <returns>height size</returns>
        public int GetHeight()
        {
            return height;
        }
    }
}
