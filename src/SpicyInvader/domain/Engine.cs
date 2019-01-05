// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 05.01.2018

using SpicyInvader.models;
using SpicyInvader.presenters.listeners;
using SpicyInvaders;
using SpicyInvaders.domain.character;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SpicyInvader.domain
{
    class Engine
    {

        // Constant
        public static int MAX_ROW_INVADERS = 6;                 // The number of row of invaders       (default : 6)                     
        public static int MAX_COLUMN_INVADERS = 10;             // The number of column of invaders       (default : 10)                     
        private const int SHOOTING_RANGE = 10;                           // The field of view of the invader to make a shoot
        private const int INTERVAL_INVADER_MISSILE_SHOOT = 1000; // default : 3000

        // Variable
        private bool invaderMissileMoving;               // Indicates if a ennemies missile is moving
        public int CurrentCloserRow { get; set; }                 // The row which ship are closer
        private PlayModel Model;                                // Reference of the model
        private Random randInvader;                      // Random object to choose a invader that wil shoot
        private ShootListener shootListener;                    // Listener


        public Engine(Model model, ShootListener listener)
        {
            // Reference of the model
            this.Model = (PlayModel) model;
            this.shootListener = listener;

            randInvader = new Random();

            // creation of a timer for generating missile from invaders
            System.Timers.Timer timer = new System.Timers.Timer(INTERVAL_INVADER_MISSILE_SHOOT);
            timer.Elapsed += new ElapsedEventHandler(OnGenerateMissile);
            timer.Start();

            
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
                            OnMoveMissile();
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

        private void OnMoveMissile()
        {
            System.Timers.Timer timer = new System.Timers.Timer(200);
            timer.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) => {
                Missile missile = Model.CurrentInvaderMissileOwner.GetMissile();
                if(missile.Y + 1 < Program.Height)
                {
                    shootListener.TempRemoveMissile();
                    missile.Move(Direction.Down);

                    // Collision management
                    if(CollisionHitBox(missile, Model.Ship)) {
                        Debug.WriteLine("Collision !! ");
                        Model.Lives--;
                        shootListener.UpdateLives();
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
    }
}
