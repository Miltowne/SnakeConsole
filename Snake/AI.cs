using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class AI : GameObject, IRendable, IMovable // Har alla attributer för att kunna röra sig, rendera ut sig
    {
        public char Object { get; set; }

        public Direction Dir { get; set; }

        public Food Food;


        public List<Tail> Tail = new List<Tail>();

        public AI(Direction dir, char o, List<Tail> tail, Position p) : base(p)
        {
            Object = o;
            Dir = dir;
            Tail = tail;
        }
        public void AiUpdate(Food Food)
        {
            this.Food = Food;
        }

        public override void Update()
        {
            

            int DiffX = Pos.X - Food.Pos.X;
            int DiffY = Pos.Y - Food.Pos.Y;

            if(Pos.X - Food.Pos.X < 1 && Pos.Y - Food.Pos.Y < 1) // är jag nära food?
            {
                if(Pos.X < Food.Pos.X) // vill jag höger?
                {
                    if(Dir == Direction.Left)
                    {
                        // kan inte bara vända till höger
                    }
                    else
                    {
                        Dir = Direction.Right;
                    }
                }
                else if(Pos.X > Food.Pos.X) // vill jag vänster?
                {
                    if(Dir == Direction.Right)
                    {
                        // kan inte bara vända till vänster
                    }
                    else
                    {
                        Dir = Direction.Left;
                    }
                }
                else if(Pos.Y > Food.Pos.Y) // vill jag upp?
                {
                    Dir = Direction.Up;
                }
                else if (Pos.Y < Food.Pos.Y) // vill jag ner?
                {
                    Dir = Direction.Down;
                }
            }
            else if (DiffX > DiffY) // är jag längre ifrån i X eller Y vinkel
            {
                if (Pos.X < Food.Pos.X) // vill vi höger?
                {
                    if (Dir == Direction.Right) // Går vi redan höger?
                    {
                        // fortsätt gå höger
                    }
                    else if (Dir == Direction.Left) // måste vi gå ett annat håll?
                    {
                        if (Pos.Y > Food.Pos.Y) // vill vi upp?
                        {
                            Dir = Direction.Up;
                        }
                        else // annars måste vi gå ner
                        {
                            Dir = Direction.Down;
                        }
                    }
                    else // vi går höger
                    {
                        Dir = Direction.Right;
                    }
                }
                else if (Pos.X > Food.Pos.X) // vill vi vänster?
                {
                    if (Dir == Direction.Left) // går vi redan vänster?
                    {
                        // fortsätt gå åt vänster
                    }
                    else if (Dir == Direction.Right) // måste vi gå ett annat håll?
                    {
                        if (Pos.Y > Food.Pos.Y) // vill vi upp?
                        {
                            Dir = Direction.Up;
                        }
                        else // annars måste vi gå ner
                        {
                            Dir = Direction.Down;
                        }
                    }
                    else
                    {
                        Dir = Direction.Left;
                    }
                }
            }
            else if (DiffX < DiffY)
            {
                if (Pos.Y > Food.Pos.Y) // vill vi upp?
                {
                    if (Dir == Direction.Up) // Går vi redan upp?
                    {
                        // fortsätt gå upp
                    }
                    else if (Dir == Direction.Down) // måste vi gå ett annat håll?
                    {
                        if (Pos.X < Food.Pos.X) // vill vi höger?
                        {
                            Dir = Direction.Right;
                        }
                        else // annars måste vi gå vänster
                        {
                            Dir = Direction.Left;
                        }
                    }
                    else // vi går upp
                    {
                        Dir = Direction.Up;
                    }
                }
                else if (Pos.Y < Food.Pos.Y) // vill vi ner?
                {
                    if (Dir == Direction.Down) // Går vi redan ner?
                    {
                        // fortsätt gå ner
                    }
                    else if (Dir == Direction.Up) // måste vi gå ett annat håll?
                    {
                        if (Pos.X < Food.Pos.X) // vill vi höger?
                        {
                            Dir = Direction.Right;
                        }
                        else // annars måste vi gå vänster
                        {
                            Dir = Direction.Left;
                        }
                    }
                    else // vi går ner
                    {
                        Dir = Direction.Down;
                    }
                }
            }

                Tail Tail = new Tail('I', new Position(Pos.X, Pos.Y));

                switch (Dir)
                {
                    case Direction.Right:
                        if (this.Tail.Count != 0)
                        {
                            this.Tail.RemoveAt(this.Tail.Count - 1);
                            this.Tail.Insert(0, Tail);
                        }
                        Pos += new Position(1, 0);

                        break; // Ändrar potition på AI beroende på Direction. Och om Tail finns så ta bort sista i listan och lägger till på gamla AI positionen
                    case Direction.Left:
                        Pos -= new Position(1, 0);
                        if (this.Tail.Count != 0)
                        {
                            this.Tail.RemoveAt(this.Tail.Count - 1);
                            this.Tail.Insert(0, Tail);
                        }
                        break;
                    case Direction.Down:
                        Pos += new Position(0, 1);
                        if (this.Tail.Count != 0)
                        {
                            this.Tail.RemoveAt(this.Tail.Count - 1);
                            this.Tail.Insert(0, Tail);
                        }
                        break;
                    case Direction.Up:
                        Pos -= new Position(0, 1);
                        if (this.Tail.Count != 0)
                        {
                            this.Tail.RemoveAt(this.Tail.Count - 1);
                            this.Tail.Insert(0, Tail);
                        }
                        break;
                    case Direction.None:
                        break;
                }
            if (Pos.X == ConsoleRenderer.Width) Pos.X = 1;

            else if (Pos.X == 0) Pos.X = ConsoleRenderer.Width - 1;

            else if (Pos.Y == ConsoleRenderer.Height) Pos.Y = 1;

            else if (Pos.Y == 0) Pos.Y = ConsoleRenderer.Height - 1; // om AI hamnar på kanten på banan så hamnar den på andra sidan

        }
    }
}
