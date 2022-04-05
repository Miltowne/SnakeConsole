using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public interface IMovable // lägger ett krav att Direction skall finnas om det ska kunna röra sig
    {
        Direction Dir { get; set; }
    }
}
