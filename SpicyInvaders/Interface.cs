using SpicyInvaders.game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SpicyInvaders
{
    class Interface
    {

        // Interface components
        Menu menu;

        // Game data
        Game game;

        // Parameters variable
        private int widthWindow;        // The width of the interface
        private int heightWindow;       // The height of the interface
        private int limitLinePosY;      // The vertical position of the limit bar

        /// <summary>
        /// Base constructor
        /// </summary>
        public Interface()
        {
            this.initialization();   // Configuration of the interface
            this.loopAnimation();
            this.keyboardHandler();  // Keyboard event handling
            
        }

        public Interface(int widthWindow, int heightWindow) 
        {
            this.widthWindow = widthWindow;
            this.heightWindow = heightWindow;
            Console.SetWindowSize(widthWindow, heightWindow);       // modify the size of the window
            Console.SetBufferSize(widthWindow, heightWindow);       // remove the scrollbar in the window

            this.initialization();   // Configuration of the interface
            this.loopAnimation();
            this.keyboardHandler();  // Keyboard event handling
        }

        /// <summary>
        /// Initialization of the user interface and the variable for the game
        /// </summary>
        private void initialization()
        {
            if (widthWindow == 0) widthWindow = Console.WindowWidth;
            if (heightWindow == 0) heightWindow = Console.WindowHeight;

            //Initialization of the "Controller"
            game = new Game(widthWindow, heightWindow);

            // Configuration of the interface
            Console.CursorVisible = false;

            // Affichage du personnage
            game.getShip().setX(Console.WindowWidth / 2);
            game.getShip().setY(Console.WindowHeight - 15);
            Console.SetCursorPosition(game.getShip().getX(), game.getShip().getY());

            // Drawing the element of the environment
            drawLimitLine();

            // Display the score - set the default score and lives
            menu = new Menu(widthWindow, heightWindow);
            menu.setLives(3);
            game.setLives(3);
            game.setScore(0);
            menu.setScore(0);

            // Drawing the game's character and object in movement
            this.redraw(Direction.Static);
        }

        /// <summary>
        /// Draw a bottom line
        /// </summary>
        private void drawLimitLine()
        {
            limitLinePosY = heightWindow - 5;
            Console.SetCursorPosition(0, limitLinePosY);

            for (int i = 0; i < widthWindow; i++)
                Console.Write("_");
        }

        /// <summary>
        /// Todo : Move this method into class
        /// Displaying of the character
        /// This method is responsible to move the ship
        /// </summary>
        private void redraw(Direction direction)
        {
            // Erasing the last character displayed on screen (at the x, y position)
            Console.SetCursorPosition(game.getShip().getX(), game.getShip().getY());
            Console.Write(' ');

            // Set the direction of the movement
            if (direction != Direction.Static)
                game.getShip().move(direction);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(game.getShip().getX(), game.getShip().getY());
            Console.Write(game.getShip().getAsciiCharacter());
        }

        /// <summary>
        /// Loop that refresh every x nanoseconds the position,
        /// animation and orientation of the object displayed on the screen
        /// Redraw accoring ot the game data.
        /// </summary>
        private void loopAnimation()
        {

            int tics = 0;
            const int fps = 5;
            const int FRAME_REFRESH = 5;

            var task = Task.Run(() =>
            {
                while (true)
                {
                    // [insert the data of the game that will be affected]
                    for (int i = game.getCurrentMissiles().Count - 1; i >= 0; i--)
                    {
                        
                        // Gestion du missile du vaisseau
                        if (game.getCurrentMissiles()[i].getOwner() is Ship && game.getCurrentMissiles()[i].isMoving)
                        {
                            Missile missile = game.getCurrentMissiles()[i];

                            // Contrôle si le ..
                            if (game.getCurrentMissiles()[i].getY() > 0)
                            {
                                // Réduction de la vitesse du missile
                                if(tics % FRAME_REFRESH == 0)
                                {
                                    //Erasing the last missile
                                    Console.SetCursorPosition(game.getCurrentMissiles()[i].getX(), game.getCurrentMissiles()[i].getY() + Missile.SPEED);
                                    Console.Write(" ");
                                    
                                    // Afficher la nouvelle position du missile
                                    Console.SetCursorPosition(game.getCurrentMissiles()[i].getX(), game.getCurrentMissiles()[i].getY());
                                    game.getCurrentMissiles()[i].setY(game.getCurrentMissiles()[i].getY() - Missile.SPEED);
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("*");

                                    // Check if an invader was touched
                                    foreach (Invader invader in game.getCurrentEnnemies())
                                    {
                                        if(!invader.IsKilled)
                                        {
                                            // Comparing the position of the missile with an ennemy
                                            if (invader.getX() == missile.getX() && invader.getY() == missile.getY() + 1)
                                            {
                                                invader.kill();
                                                game.dishoot();                                 // Destroy the missile
                                                game.setScore(invader.getScoreGain());
                                                menu.setScore(game.getScore());
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Erasing the last missile
                                Debug.WriteLine(missile.getX() + "_" + missile.getY());
                                this.eraseChar(missile.getX(), missile.getY() + Missile.SPEED);

                                // End of the missile - Kill it
                                game.dishoot();
                            }
                        }
                    }
             
                    // Temporisation fps
                    Thread.Sleep(fps);

                    // Vitesse missile
                    tics++;
                    if (tics == Int32.MaxValue)
                        tics = 0;
                }
            });

            // Affichage des ennemies
            foreach(Invader invader in game.getCurrentEnnemies())
            {
                Console.SetCursorPosition(invader.getX(), invader.getY());
                Console.Write(invader.getAsciiCharacter());
            }

            // Déplacement des ennemies
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            // Change the position
            Debug.WriteLine("Time :" + e.SignalTime);
        }

        /// <summary>
        /// //Erasing the last missile
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        private void eraseChar(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write(" ");
        }

        /// <summary>
        /// Handling the keyboard event
        /// </summary>
        private void keyboardHandler()
        {
            while (game.getShip().isAlive())
            {

                ConsoleKey consoleKey = Console.ReadKey(true).Key;

                switch (consoleKey)
                {
                    case ConsoleKey.LeftArrow:
                        Ship ship = (Ship) game.getShip();
                        if(!ship.GetMissile().isMoving)
                            redraw(Direction.Left);
                        break;

                    case ConsoleKey.RightArrow:
                        ship = (Ship) game.getShip();
                        if(!ship.GetMissile().isMoving)
                            redraw(Direction.Right);
                        break;

                    case ConsoleKey.Spacebar:
                        game.shoot();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
