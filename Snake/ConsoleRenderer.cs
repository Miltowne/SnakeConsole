using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class ConsoleRenderer
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        private GameWorld world; // vi fixar en privat variabel av hela gameworld där vi kan nå variablev och funktioner
        public ConsoleRenderer(GameWorld gameWorld, int width, int height)
        {
            Height = height;
            Width = width;
            world = gameWorld;
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height); // sätter världens storlek
        }
        
        public void Render()
        {
            if(world.Snake != null)
            {
                foreach (var item in world.Snake.Tail) // Rendera alla Tail object till skärmen
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition((item as Tail).Pos.X, (item as Tail).Pos.Y);
                    Console.Write((item as Tail).Object);
                }
            }

            if(world.Ai != null)
            {
                foreach (var item in world.Ai.Tail) // Rendera alla Tail object till skärmen
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition((item as Tail).Pos.X, (item as Tail).Pos.Y);
                    Console.Write((item as Tail).Object);
                }
            }
           

            foreach (var item in world.Collection) // Rendera alla Object som är medlem av IRendable
            {
                if (item is IRendable)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(item.Pos.X, item.Pos.Y);
                    Console.Write((item as IRendable).Object);
                }
            }
            Console.CursorVisible = false; // tar bort blinkande muspekaren
            Console.SetCursorPosition(0,0);
            Console.Write($"Dina poäng är nu {world.Points}"); // sätter dina aktuella poäng uppe i vänstra hörnet
            Console.SetCursorPosition(0, Height-1);
            Console.Write($"Tid kvar {world.Time}"); // timer att ta maten innan man förlorar
        }
        public void RenderBlanc()
        {
            Console.SetCursorPosition(10, Height - 1); // fulfix för att när world.Time går från 2 siffrigt till 1 siffrigt så är renderade 0:an kvar så det står t.ex 90
            Console.Write(' ');

            if (world.Snake != null)
            {
                foreach (var item in world.Snake.Tail) // RenderBlanc:a alla Tail object från skärmen
                {
                    Console.SetCursorPosition((item as Tail).Pos.X, (item as Tail).Pos.Y);
                    Console.Write(' ');

                }
            }

            if (world.Ai != null)
            {
                foreach (var item in world.Ai.Tail) // Rendera alla Tail object till skärmen
                {
                    Console.SetCursorPosition((item as Tail).Pos.X, (item as Tail).Pos.Y);
                    Console.Write(' ');
                }
            }

            foreach (var item in world.Collection) // RenderBlanc:a alla IRendable object
            {
                Console.SetCursorPosition(item.Pos.X, item.Pos.Y);
                Console.Write(' ');
            }
        }
    }
}