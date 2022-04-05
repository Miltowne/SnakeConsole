using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public abstract class GameObject
    {

        public Position Pos; // Pos kommer aggera som Position 
        public GameObject(Position p)
        {
            Pos = p;
        }
        

        public virtual void Update()
        {
            
        }
    }
}
