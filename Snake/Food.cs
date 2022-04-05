using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class Food: GameObject, IRendable
    {

        public char Object { get; set; }

        public Food(Position p, char o) : base(p)
        {
            Object = o;
        }
        public override void Update()
        {
            
        }
    }
}
