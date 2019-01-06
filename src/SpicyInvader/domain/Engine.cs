// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 05.01.2018

using SpicyInvader.models;
using SpicyInvader.presenters.listeners;
using SpicyInvader.views.utils;
using SpicyInvaders;
using SpicyInvaders.domain.character;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SpicyInvader.domain
{
    class Engine
    {

        // Constant
        public static int MAX_ROW_INVADERS = 6;                 // The number of row of invaders       (default : 6)                     
        public static int MAX_COLUMN_INVADERS = 10;             // The number of column of invaders       (default : 10)                     
        public static int MAX_INVADER_MOVE;     // The maximum number of times invaders can move, is the screenwidth - width of invaders'bloc size
        public const short SPACE_BETWEEN_INVADER = 3;                          // the space that is between the ennemies displayed
        private const short LIMIT_MISSILE_DESTROY = 0;                          // The limit where a missile can still be drawed on screen

        private const int SHOOTING_RANGE = 10;                           // The field of view of the invader to make a shoot
        private const int INTERVAL_INVADER_MISSILE_SHOOT = 1000; // default : 3000

        // Variable
        private bool gameOver;
        private bool invaderMissileMoving;               // Indicates if a ennemies missile is moving
        public int CurrentCloserRow { get; set; }                 // The row which ship are closer
        private PlayModel Model;                                // Reference of the model
        
        private ShootListener shootListener;                    // Listener

        private int currentMoveCount;                   // Count the number of times invaders moved
        private Direction currentMoveSens;              // The sens ( <- left or right -> ) where the invaders are going

        private Random randInvader;                      // Random object to choose a invader that wil shoot
        private Timer timerInvadersMissile;              // Timer for moving invader's missile
        private Timer timerShipMissile;                 // Timer for moving missile of Allied Ship
        private Timer timerMovingInvaders;
        private bool isInvaderTransitioning;

        public Engine(Model model, ShootListener listener)
        {
            // Reference of the model
            this.Model = (PlayModel) model;
            this.shootListener = listener;

            randInvader = new Random();

            currentMoveCount = 0;
            currentMoveSens = Direction.Right;

            // Timer for moving invaders
            timerMovingInvaders = new System.Timers.Timer(700);
            timerMovingInvaders.Elapsed += new ElapsedEventHandler(OnMoveInvaders);
            timerMovingInvaders.Start();

            // creation of a timer for generating missile from invaders
            timerInvadersMissile = new System.Timers.Timer(INTERVAL_INVADER_MISSILE_SHOOT);
            timerInvadersMissile.Elapsed += new ElapsedEventHandler(OnGenerateMissile);
            timerInvadersMissile.Start();
        }

        public void Stop()
        {
            timerInvadersMissile.Stop();
            timerMovingInvaders.Stop();
            gameOver = true;
        }

        /// <summary>
        /// Start the operating of shooting from the Allied Ship.
        /// </summary>
        public void ShipShooting()
        {
            if(!Model.Ship.GetMissile().isMoving)
            {
                Model.Ship.Shoot(true);

                // creation of a timer for generating missile from invaders
                timerShipMissile = new System.Timers.Timer(75);
                timerShipMissile.Elapsed += new ElapsedEventHandler(OnMoveShipMissile);
                timerShipMissile.Start();
            }
        }

        /// <summary>
        /// Event handler for moving the missile of the Allied Ship.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnMoveShipMissile(object source, ElapsedEventArgs e)
        {
            // Remove the missile from screen
            Missile missile = Model.Ship.GetMissile();
            ConsoleUtils.RemoveChar(missile.X, missile.Y);
            //shootListener.TempRemoveMissile();

            // Move the missile
            missile.Move(Direction.Up);

            // Check if the missile goes outside the limits of the console size
            if (missile.Y < LIMIT_MISSILE_DESTROY)
            {
                Model.Ship.Shoot(false);
                timerShipMissile.Stop();
            }else
            {
                if(Model.Ship.GetMissile().isMoving)
                {
                    // Collision management
                    DisplayableObject[] invaderAlive = this.getInvadersAlive().ToArray();
                    Invader invader = (Invader) CollisionObjectsHitBox(missile, invaderAlive);
                    if (invader != null)
                    {
                        timerShipMissile.Stop();
                        Model.Ship.Shoot(false);
                        invader.Kill();
                        shootListener.OnInvaderKilled(invader);

                        Model.Score += invader.GetScoreGain();
                        shootListener.UpdateMenu();
                    }
                    else
                    {
                        // Callback Listener
                        shootListener.UpdateShipPosition();
                    }
                }
            }
        }

        /// <summary>
        /// Generate a new missile from the space invader at the same y position of the ship
        /// </summary>
        /// <param name=""></param>
        private void OnGenerateMissile(object source, ElapsedEventArgs e)
        {
            
            List<Invader> invaders = GetInvadersAtRow(GetCloserInvadersRow());

            while (true)
            {
                if (!invaderMissileMoving)
                {
                    int pos = randInvader.Next(0, invaders.Count);

                    if (invaders[pos].IsAlive)
                    {
                        if (invaders[pos].X >= Model.Ship.X - SHOOTING_RANGE && invaders[pos].X <= Model.Ship.X + SHOOTING_RANGE)
                        {
                            invaderMissileMoving = true;
                            invaders[pos].Shoot(true);

                            Model.CurrentInvaderMissileOwner = invaders[pos];
                            shootListener.OnShoot();
                            onMoveInvadersMissile();
                        }
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            
        }

        private void OnMoveInvaders(object source, ElapsedEventArgs e)
        {
            // Déplacement des ennemies
            // Change the position of invaders

            Direction sens = currentMoveSens;                     // sens of the moving (left or right)

            // Settings the sens of the invaders -----------------------------------------------------------
            // if the number of movement is lower than the maximum, we enable movement
            if (currentMoveCount < MAX_INVADER_MOVE)
            {
                currentMoveCount++;
            }
            else
            {
                // on descend en bas
                if (!isInvaderTransitioning)
                {
                    isInvaderTransitioning = true;
                    currentMoveSens = (currentMoveSens == Direction.Right) ? Direction.Left : Direction.Right;
                    sens = Direction.Down;
                }
                // On continue dans l'autre sens
                else
                {
                    isInvaderTransitioning = false;
                    currentMoveCount = 0;
                }
            }

            // Affecting the new position of invaders -----------------------------------------------------------
            foreach (Invader invader in Model.Invaders)
            {
                if (invader.IsAlive)
                {
                    if (CollisionHitBox(Model.Ship, invader))
                    {
                        Model.Lives = 0;
                        shootListener.UpdateMenu();
                        shootListener.GameOver();

                    }
                    else
                    {
                        // erasing the last character
                        ConsoleUtils.RemoveChar(invader.X, invader.Y);

                        // Changing 
                        invader.Move(sens);

                        // drawing the new character
                        ConsoleUtils.FastDraw(invader.X, invader.Y, invader);
                    }
                }
            }
        }

        private void onMoveInvadersMissile()
        {
            System.Timers.Timer timer = new System.Timers.Timer(75);
            timer.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) => {
                Missile missile = Model.CurrentInvaderMissileOwner.GetMissile();
                if(missile.Y + 1 < Program.Height && !gameOver)
                {
                    // Remove the missile from screen
                    shootListener.TempRemoveMissile();

                    // Move the missile
                    missile.Move(Direction.Down);

                    // Collision management
                    if(CollisionHitBox(missile, Model.Ship)) {
                        Model.Lives--;
                        shootListener.UpdateMenu();

                        if(Model.Lives == 0)
                        {
                            // Prevent for the end of the game
                            shootListener.GameOver();
                        }
                    }

                    // Callback Listener
                    shootListener.OnShoot();
                }else
                {
                    timer.Stop();
                    Model.CurrentInvaderMissileOwner = null;
                    invaderMissileMoving = false;
                }
                
            });
            timer.Start();
        }

        /// <summary>
        /// Check collision between a DisplayableObject and a set of Displayable object
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="dSet"></param>
        /// <returns></returns>
        private DisplayableObject CollisionObjectsHitBox(DisplayableObject d1, DisplayableObject[] dSet)
        {
            foreach(DisplayableObject d2 in dSet)
            {
                if (CollisionHitBox(d1, d2))
                    return d2;
            }

            return null;
        }

        /// <summary>
        /// Return boolean that indicates,
        /// if position of Ship is Equal to position of the current Missile of invader
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        private bool CollisionHitBox(DisplayableObject d1, DisplayableObject d2)
        {
            if (d1 != null && d2 != null)
                return (d1.X == d2.X & d1.Y == d2.Y);

            return false;
        }


        /// <summary>
        /// Return a list of invaders from a specific row
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private List<Invader> GetInvadersAtRow(int rowIndex)
        {
            List<Invader> invaders = new List<Invader>();

            foreach (Invader invader in Model.Invaders)
            {
                if (invader.Row == rowIndex)
                {
                    invaders.Add(invader);
                }
            }

            return invaders;
        }

        public int GetCloserInvadersRow()
        {
            for (int i = 0; i < MAX_ROW_INVADERS; i++)
            {
                List<Invader> invaders = GetInvadersAtRow(MAX_ROW_INVADERS);

                foreach (Invader invader in invaders)
                {
                    if (invader.IsAlive)
                    {
                        CurrentCloserRow = MAX_ROW_INVADERS;
                        return CurrentCloserRow;
                    }
                }
            }

            return CurrentCloserRow;
        }

        /// <summary>
        /// Return a List of Invaders still alive.
        /// </summary>
        /// <returns></returns>
        public List<Invader> getInvadersAlive()
        {
            List<Invader> invaders = new List<Invader>();

            foreach (Invader invader in Model.Invaders)
            {
                if (invader.IsAlive)
                {
                    invaders.Add(invader);
                }
            }

            return invaders;
        }
    }
}
