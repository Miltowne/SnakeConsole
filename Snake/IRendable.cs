using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public interface IRendable // lägger ett krav på att ett object ska finnas om det ska kunna renderas
    {
        char Object { get; set; }
    }
}
