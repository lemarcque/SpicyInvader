// Author : Henoc Sese
// Description : Space Invaders game
// Date : 19.11.2018
// Locality : Lausanne 

using SpicyInvaders.game;
using System;
using System.Diagnostics;
using System.Threading;

namespace SpicyInvaders
{
    /// <summary>
    /// (View) : Manage all the components of the UI (User Interface)
    /// and display data returned from the Controller object (Game)
    /// </summary>
    class Interface
    {

        // Interface components
        Menu menu;

        // Game data
        Game game;
        
        private int currentMoveCount;        // Count the number of times invaders moved
        private Direction currentMoveSens;  // The sens ( <- left or right -> ) where the invaders are going
        private bool isInvaderTransitioning;

        // Parameters variable
        public static int WIDTH_WINDOW;        // The width of the interface
        public static int HEIGHT_WINDOW;       // The height of the interface
        private int limitLinePosY;      // The vertical position of the limit bar
        private const short LIMIT_MISSILE_DESTROY = 3;

        /// <summary>
        /// Base constructor
        /// </summary>
        public Interface()
        {
            this.initialization();   // Configuration of the interface
            //this.loopAnimation();
            this.keyboardHandler();  // Keyboard event handling
            
        }

        public Interface(int widthWindow, int heightWindow) 
        {
            WIDTH_WINDOW = widthWindow;
            HEIGHT_WINDOW = heightWindow;
            Console.SetWindowSize(widthWindow, heightWindow);       // modify the size of the window
            Console.SetBufferSize(widthWindow, heightWindow);       // remove the scrollbar in the window

            this.initialization();   // Configuration of the interface
            this.loopAnimation();
            Console.Read();
        }

        /// <summary>
        /// Initialization of the user interface and the variable for the game
        /// </summary>
        private void initialization()
        {
            if (WIDTH_WINDOW == 0) WIDTH_WINDOW = Console.WindowWidth;
            if (HEIGHT_WINDOW == 0) HEIGHT_WINDOW = Console.WindowHeight;

            //Initialization of the "Controller"
            game = new Game(WIDTH_WINDOW, HEIGHT_WINDOW);

            // Configuration of the interface
            Console.CursorVisible = false;

            // Displaying the character
            game.getShip().setX(Console.WindowWidth / 2);
            game.getShip().setY(Console.WindowHeight - 15);
            Console.SetCursorPosition(game.getShip().getX(), game.getShip().getY());

            // Drawing the element of the environment
            drawLimitLine();

            // Display the score - set the default score and lives
            menu = new Menu(WIDTH_WINDOW, HEIGHT_WINDOW);
            menu.setLives(Game.LIVES);
            game.setLives(Game.LIVES);
            game.setScore(0);
            menu.setScore(0);

            // Drawing the game's character and object in movement
            this.redraw(Direction.Static);

            // Initialization of the game data
            currentMoveCount = 0;
            currentMoveSens = Direction.Right;
        }

        /// <summary>
        /// Draw a bottom line
        /// </summary>
        private void drawLimitLine()
        {
            limitLinePosY = HEIGHT_WINDOW - 5;
            Console.SetCursorPosition(0, limitLinePosY);

            for (int i = 0; i < WIDTH_WINDOW; i++)
            {
                Console.Write("_");
            }
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
            {
                game.getShip().move(direction);
            }


            // Drawing
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
            const int fps = 10;
            const int FRAME_REFRESH = 5;

            // timer for ennemies moving (refresh of their position)
            short speedInvaders = 10;

            // Displaying (for first time) the ennemies on the "scene"
            foreach (Invader invader in game.getCurrentEnnemies())
            {
                writeChar(invader.getX(), invader.getY(), invader.getAsciiCharacter());
            }

            redraw(Direction.Static);

            // Handling keyboard events
            while (game.getShip().isAlive())
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        
                        case ConsoleKey.LeftArrow:
                        {
                            redraw(Direction.Left);
                            break;
                        }

                        case ConsoleKey.RightArrow:
                        {
                            redraw(Direction.Right);
                            break;
                        }

                        case ConsoleKey.Spacebar:
                        {
                            game.shoot();
                            break;
                        }
                                                   

                        default:
                        {
                            break;
                        }
                            
                    }

                }

                // parcours la liste des missiles qui sont lancés
                for (int i = game.getCurrentMissiles().Count - 1; i >= 0; i--)
                {
                    Missile missile = game.getCurrentMissiles()[i];

                    // Handling missile 
                    // Ship missile management
                    if (game.getCurrentMissiles()[i].getOwner() is Ship && game.getCurrentMissiles()[i].isMoving)
                    {


                        // Check if the missile goes outside the limits of the console size
                        if (game.getCurrentMissiles()[i].getY() > LIMIT_MISSILE_DESTROY)
                        {
                            // Réduction de la vitesse du missile
                            if(tics % FRAME_REFRESH == 0)
                            {
                                //Erasing the last missile
                                eraseChar(game.getCurrentMissiles()[i].getX(), game.getCurrentMissiles()[i].getY() + Missile.SPEED);
                                    
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

                    //  Management of the missiles of the "invaders"
                    else
                    {
                        Debug.WriteLine("Owner : " + missile.getOwner());
                    }

                }
             
                // Temporisation fps
                Thread.Sleep(fps);

                // Déplacement des ennemies
                // Change the position of invaders
                
                if(tics % speedInvaders == 0)
                {
                    InvaderIsMoving = true;
                    Direction sens = currentMoveSens;                     // sens of the moving (left or right)

                    // Settings the sens of the invaders -----------------------------------------------------------
                    // if the number of movement is lower than the maximum, we enable movement
                    if (currentMoveCount < Game.MAX_INVADER_MOVE)
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
                    foreach (Invader invader in game.getCurrentEnnemies())
                    {
                        if (!invader.IsKilled)
                        {
                            if(game.getShip().OnCollision(invader)) {

                                game.setLives(0);
                               // menu.setLives(game.getLives());
                                game.finish();

                                // STOP THE GAME
                                Debug.WriteLine("Stop the game");
                            }else
                            {

                                // erasing the last character
                                eraseChar(invader.getX(), invader.getY());
                                invader.move(sens);

                                // drawing the new character
                                Console.ForegroundColor = ConsoleColor.Gray;
                                writeChar(invader.getX(), invader.getY(), invader.getAsciiCharacter());
                            }
                        }
                    }
                }

                // Vitesse missile
                tics++;
                if (tics == Int32.MaxValue)
                {
                    tics = 0;
                }
            }
        }
        

        /// <summary>
        /// // Erasing the last missile ath the specified position
        /// </summary>
        /// <param name="posX">Correspond the X position</param>
        /// <param name="posY">Correspond the Y position></param>
        private void eraseChar(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write(" ");
        }

        private void writeChar(int posX, int posY, string asciichar)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write(asciichar);
        }


        bool InvaderIsMoving;

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
                        if (!ship.GetMissile().isMoving)
                        {
                            redraw(Direction.Left);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        ship = (Ship) game.getShip();
                        if(!ship.GetMissile().isMoving){
                             redraw(Direction.Right);
                        }
                        break;

                    case ConsoleKey.Spacebar:
                        if (!InvaderIsMoving)
                        {
                            game.shoot();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
