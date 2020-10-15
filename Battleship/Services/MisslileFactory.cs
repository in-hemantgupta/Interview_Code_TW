using Battleship.Models;
using Battleship.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Services
{
    public class MissileFactory : IMissileFactory
    {
        Func<MissileType, IMissile> _missiles;
        public MissileFactory(Func<MissileType, IMissile> missiles)
        {
            this._missiles = missiles;
        }

        public IMissile CreateMissile(MissileType missileType) {
            return _missiles(missileType);
        }
    }
}
