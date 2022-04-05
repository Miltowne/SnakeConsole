using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Points //innehåller poäng som player har skaffat sig
    {
        public int Point { get; set; } 
        public Points(int point)
        {
            Point = point;
        }
    }
}
