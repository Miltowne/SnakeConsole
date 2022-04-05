using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class Tail: GameObject, IRendable // Skapar klassen tail som går att renderas (IRendable) och har ett object som ska renderas (GameObject)
    {
        public char Object { get; set; }

        public Tail(char o, Position p): base(p)
        {
            Object = o;
        }
    }
}
