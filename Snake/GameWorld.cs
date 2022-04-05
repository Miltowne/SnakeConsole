using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class GameWorld
    {
        public int frameRate;
        public int Points { get; set; } // Poäng för spelaren 

        public int Time = 15; // Tid som ska tickas ned

        public List<GameObject> Collection { get;private set; } = new List<GameObject> { }; // Lista som ska innehålla alla spelobjekt
        public Food Food { get; set; } // variabel för Food
        public Player Snake { get; set; } // variabel för Spelaren 

        public AI Ai { get; set; } // variabel för AI som kommer aggera som en Snake

        /// <summary>
        /// skapar object till spelvärlden. Allt beroende på om spelaren väljer för choice. Lägger till dem i Collection
        /// </summary>
        /// <param name="Choice"></param>
        public void CreateObjects(int Choice)
        {
            if (Choice == 0)
            {
                List<Tail> Tail = new List<Tail>();
                Player snake = new Player(Direction.Right, new Position(20,25), 'O', Tail);
                Collection.Add(snake);
                Snake = snake;

                Random random = new Random();
                int ranX = random.Next(1, ConsoleRenderer.Width);
                int ranY = random.Next(1, ConsoleRenderer.Height);
                Food Food = new Food(new Position(ranX, ranY), 'ô'); // skapar en ny Food med random kordinat (men inom banans storlek)

                this.Food = Food;
                Collection.Add(Food); // this.Food får nya värdet, gamla Food tas bort från Collection, nya Food kommer in
            }
            else if (Choice == 1)
            {
                List<Tail> Tail = new List<Tail>();
                AI Ai = new AI(Direction.Right, 'A', Tail,  new Position(20,25));
                Collection.Add(Ai);
                this.Ai = Ai;

                Random random = new Random();
                int ranX = random.Next(1, ConsoleRenderer.Width);
                int ranY = random.Next(1, ConsoleRenderer.Height);
                Food Food = new Food(new Position(ranX, ranY), 'ô'); // skapar en ny Food med random kordinat (men inom banans storlek)

                this.Food = Food;
                Collection.Add(Food); // this.Food får nya värdet, gamla Food tas bort från Collection, nya Food kommer in
            }
            else
            {
                Random random = new Random();
                int ranX = random.Next(1, ConsoleRenderer.Width);
                int ranY = random.Next(1, ConsoleRenderer.Height);
                Food Food = new Food(new Position(ranX, ranY), 'ô'); // skapar en ny Food med random kordinat (men inom banans storlek)

                this.Food = Food;
                Collection.Add(Food); // this.Food får nya värdet, gamla Food tas bort från Collection, nya Food kommer in
            }
        }
        public void CreateFood()
        {
            if (Snake != null) // kolla om Snake ens har skapats
            {
                if (Snake.Pos == this.Food.Pos) // tar Snake maten?
                {
                    SpeedUp();
                    CreateTail();// Snake ska få ett Tail objekt
                    Time = 15;// tiden reset:as
                    Points++;// vi får en poäng
                    Random random = new Random();
                    int ranX = random.Next(1, ConsoleRenderer.Width);
                    int ranY = random.Next(1, ConsoleRenderer.Height);
                    Food Food = new Food(new Position(ranX, ranY), 'ô'); // skapar en ny Food med random kordinat (men inom banans storlek)

                    this.Food = Food;
                    this.Collection.RemoveAt(1);
                    Collection.Add(Food); // this.Food får nya värdet, gamla Food tas bort från Collection, nya Food kommer in

                }
            }
            if (Ai != null) // kollar om AI ens har skapats
            {
                if (Ai.Pos == this.Food.Pos) // tar Ai maten?
                {
                    SpeedUp();
                    CreateTailAI(); // AI ska få ett Tail objekt
                    Time = 15; // tiden reset:as
                    Points++; // vi får en poäng
                    Random random = new Random();
                    int ranX = random.Next(1, ConsoleRenderer.Width-1);
                    int ranY = random.Next(1, ConsoleRenderer.Height-1);
                    Food Food = new Food(new Position(ranX, ranY), 'ô'); // skapar en ny Food med random kordinat (men inom banans storlek)

                    this.Food = Food;
                    this.Collection.RemoveAt(1);
                    Collection.Add(Food); // this.Food får nya värdet, gamla Food tas bort från Collection, nya Food kommer in
                }
            }

        }
        /// <summary>
        /// skapar ett Tail object och lägger in i Snake.Tail Listan beroende på vart vi "får plats"
        /// </summary>
        /// (Buggfix) Ändrar vi inte Tail.Pos efter vilken Direction Snake går åt så hinner GameOver() tro att vi förlorat
        public void CreateTail()
        {
            if (Snake.Dir == Direction.Right)
            {
                Tail Tail = new Tail('O', new Position(Snake.Pos.X - 1, Snake.Pos.Y));
                Snake.Tail.Insert(0, Tail);
            }
            else if (Snake.Dir == Direction.Left)
            {
                Tail Tail = new Tail('O', new Position(Snake.Pos.X + 1, Snake.Pos.Y));
                Snake.Tail.Insert(0, Tail);
            }
            else if (Snake.Dir == Direction.Down)
            {
                Tail Tail = new Tail('O', new Position(Snake.Pos.X, Snake.Pos.Y - 1));
                Snake.Tail.Insert(0, Tail);
            }
            else if (Snake.Dir == Direction.Up)
            {
                Tail Tail = new Tail('O', new Position(Snake.Pos.X, Snake.Pos.Y + 1));
                Snake.Tail.Insert(0, Tail);
            }
        }
        /// <summary>
        /// skapar ett Tail object och lägger in i AI.Tail Listan beroende på vart vi "får plats"
        /// </summary>
        /// (Buggfix) Ändrar vi inte Tail.Pos efter vilken Direction Snake går åt så hinner GameOver() tro att vi förlorat
        public void CreateTailAI()
        {
            if (Ai.Dir == Direction.Right)
            {
                Tail Tail = new Tail('I', new Position(Ai.Pos.X - 1, Ai.Pos.Y));
                Ai.Tail.Insert(0, Tail);
            }
            else if (Ai.Dir == Direction.Left)
            {
                Tail Tail = new Tail('I', new Position(Ai.Pos.X + 1, Ai.Pos.Y));
                Ai.Tail.Insert(0, Tail);
            }
            else if (Ai.Dir == Direction.Down)
            {
                Tail Tail = new Tail('I', new Position(Ai.Pos.X, Ai.Pos.Y - 1));
                Ai.Tail.Insert(0, Tail);
            }
            else if (Ai.Dir == Direction.Up)
            {
                Tail Tail = new Tail('I', new Position(Ai.Pos.X, Ai.Pos.Y + 1));
                Ai.Tail.Insert(0, Tail);
            }
        }
        /// <summary>
        /// Anropas av Program.Loop() för att se om vi förlorat spelet. return false om vi förlorar det fallet
        /// </summary>
        /// <returns>true/false</returns>
        public bool GameOver()
        {
            if(Time == 0)
            {
                Console.WriteLine("GameOver");
                Console.ReadKey();
                return false;
            }
            if (Snake != null) // dubbelkollar om Snake ens har skapats
            {
                foreach (var item in Snake.Tail) // loopar alla object i Listan Tail för att kolla igenom alla Tail objekt
                {
                    if (item.Pos == Snake.Pos)
                    {
                        Console.WriteLine("GameOver");
                        Console.ReadKey();
                        return false;
                    }
                }
            }
            else if(Ai != null) // dubbelkollar om AI ens har skapats
            {
                foreach (var item in Ai.Tail) // loopar alla object i Listan Tail för att kolla igenom alla Tail objekt
                {
                    if (item.Pos == Ai.Pos)
                    {
                        Console.WriteLine("GameOver");
                        Console.ReadKey();
                        return false;
                    }
                }
            }
            
            return true;
        }
        public void TimeDown() // anropas av Program.Loop() för att minska "klockan" Time
        {
            Time--;
        }

        public void SpeedUp()
        {
            frameRate++;
        }

        public void Update()
        {
            CreateFood(); // kollar om ny food ska skapas lr om gamla är kvar 
            if(Ai != null)
            {
                Ai.AiUpdate(Food); // skickar ny info om Food till Ai
            }
            foreach (var item in Collection) // går igenom alla object i Collection och anropar deras Update() metod
            {
                item.Update();
            }
        }
    }
}

