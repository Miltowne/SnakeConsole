using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake
{
        public enum Direction { Right, Left, Down, Up, None }; // skapar enum för direction som kommer styra alla object som ska kunna styras (IMovable)

    public class Program
    {
        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it as uppercase, otherwise returns '\0'.
        /// </summary>
        static char ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key.ToString().ToUpper()[0] : '\0';
        static void Loop(int Choice)
        {
            // Initialisera spelet
            int frameRate = 5;
            int TimeDelay = frameRate; // variabel för att kunna styra GameWorld.TimePlus var 5:e varv i loopen (blir bättre "klocka")
            GameWorld world = new GameWorld();
            ConsoleRenderer renderer = new ConsoleRenderer(world, 30, 35);
            world.CreateObjects(Choice);

            // Huvudloopen
            
            bool running = true;
            while (running)
            {
                // Kom ihåg vad klockan var i början
                DateTime before = DateTime.Now;

                // Hantera knapptryckningar från användaren
                char key = ReadKeyIfExists();
                switch (key)
                {
                    case 'Q':
                        running = false; // hanterar Direction för Snake (spelaren) beroende på knapptryckning
                        break;
                    case 'W':
                        if(world.Snake.Dir == Direction.Down)
                        {
                            break;
                        }
                        else
                        {
                            world.Snake.Dir = Direction.Up;
                        }
                        break;
                    case 'A':
                        if (world.Snake.Dir == Direction.Right)
                        {
                            break;
                        }
                        else
                        {
                            world.Snake.Dir = Direction.Left;
                        }
                        break;
                    case 'S':
                        if (world.Snake.Dir == Direction.Up)
                        {
                            break;
                        }
                        else
                        {
                            world.Snake.Dir = Direction.Down;
                        }
                        break;
                    case 'D':
                        if (world.Snake.Dir == Direction.Left)
                        {
                            break;
                        }
                        else
                        {
                            world.Snake.Dir = Direction.Right;
                        }
                        break;

                }
                TimeDelay--;
                if(TimeDelay == 0) // TimeDown ska köras med en Delay av samma värde av den satta frameRate:n
                {
                    world.TimeDown();
                    TimeDelay = frameRate;
                }
                // Uppdatera världen och rendera om
                renderer.RenderBlanc(); // börjar med att köra blanc på allt innan som renderats
                world.Update(); // uppdaterar alla vaiabler, player, food osv
                renderer.Render(); // kör render för att få ut alla nya object till skärmen
                running = world.GameOver(); // kollar med metoden GameOver om vi förlorat i spelet och running får false så loopen bryts

                // Mät hur lång tid det tog
                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);
                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Thread.Sleep((int)frameTime);
                }
            }

        }
            

        static void Main(string[] args)
        {
            Loop(Menu());
        }

        /// <summary>
        /// Snygg meny med möjlighet att välja mellan AI lr Player som ska spela som våran Snake
        /// </summary>
        /// <returns></returns>
        static int Menu()
        {
            int Choice = 1;
            string Welcome = @" _____             _        
/  ___|           | |       
\ `--. _ __   __ _| | _____ 
 `--. \ '_ \ / _` | |/ / _ \
/\__/ / | | | (_| |   <  __/
\____/|_| |_|\__,_|_|\_\___|
                            
                            ";
            Console.WriteLine(Welcome);
            Console.WriteLine("Välkommen till snake");
            Console.WriteLine("Player eller AI som spelare?");
            Console.WriteLine("Skriv 1 för AI, skriv 0 för att spela själv");
            Console.WriteLine("styr med W A S D knapparna");
            Choice = int.Parse(Console.ReadLine());
            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
            Console.Clear();
            return Choice;
        }
    }
}
