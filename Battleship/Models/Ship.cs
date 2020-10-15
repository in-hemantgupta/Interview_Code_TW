using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Models
{
    public class Ship
    {
        public ShipType ShipType { get; set; }


        public List<Coordinates> Coordinates { get; set; }
        private short _life;
        public short Life
        {
            get { return _life; }
            set
            {
                this._life = value;
                if (this._life == 0)
                {
                    //Destroy the ship when all life goest to 0
                    this.IsDestroyed = true;
                }
            }
        }
        public bool IsDestroyed { get; set; }

        public Ship(ShipType type)
        {
            this.ShipType = type;
        }
    }
}
