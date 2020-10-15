using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Services.Contracts
{
    interface IMissileService
    {
        int Attack(Ship ship);
    }
}
