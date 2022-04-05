using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    /// <summary>
    /// Player klass som är medlem för att kunda renderas ut på skärmen och kunna röras i Direction
    /// </summary>
    public class Player : GameObject, IRendable, IMovable 
    {

        public Direction Dir { get; set; }

        public char Object { get; set; }

        public List<Tail> Tail = new List<Tail>();
        public Player(Direction dir, Position p, char o, List<Tail> tail) : base(p) // för att skapa en player krävs Dir, Pos, char och en lista av Tail object
        {
            Object = o;
            Dir = dir;
            Tail = tail;
        }
        public override void Update()
        {
            
            

            switch (Dir)
            {
                case Direction.Right:
                    if (this.Tail.Count != 0)
                    {
                        this.Tail.RemoveAt(this.Tail.Count-1);
                        Tail Tail = new Tail('O', new Position(Pos.X, Pos.Y));  // new position = gamla Players position
                        this.Tail.Insert(0, Tail); 
                    }
                    Pos += new Position(1, 0); // ger player ny position beroende på Direction
                    break; // Ändrar potition på Snake beroende på Direction. Och om Tail finns så ta bort sista i listan och lägger till på gamla Player positionen 
                case Direction.Left:
                    Pos -= new Position(1, 0);
                    if (this.Tail.Count != 0)
                    {
                        this.Tail.RemoveAt(this.Tail.Count-1);
                        Tail Tail = new Tail('O', new Position(Pos.X + 1, Pos.Y));
                        this.Tail.Insert(0, Tail);
                    } 
                    break;
                case Direction.Down:
                    Pos += new Position(0, 1);
                    if (this.Tail.Count != 0)
                    {
                        this.Tail.RemoveAt(this.Tail.Count-1);
                        Tail Tail = new Tail('O', new Position(Pos.X, Pos.Y - 1));
                        this.Tail.Insert(0, Tail);
                    } 
                    break;
                case Direction.Up:
                    Pos -= new Position(0, 1);
                    if (this.Tail.Count != 0)
                    {
                        this.Tail.RemoveAt(this.Tail.Count-1);
                        Tail Tail = new Tail('O', new Position(Pos.X, Pos.Y + 1));
                        this.Tail.Insert(0, Tail);
                    }
                    break;
                case Direction.None:
                    break;
            }
            if (Pos.X == ConsoleRenderer.Width) Pos.X = 1;

            else if (Pos.X == 0) Pos.X = ConsoleRenderer.Width - 1;

            else if (Pos.Y == ConsoleRenderer.Height) Pos.Y = 1;

            else if (Pos.Y == 0) Pos.Y = ConsoleRenderer.Height - 1; // om Player hamnar på kanten på banan så hamnar den på andra sidan
        }

    }
}
