using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Services
{
    public class BattleshipService : IBattleshipService
    {
        #region Properties
        public Area BattleshipArea { get; set; }
        public List<Player> Players { get; set; }
        IPlayerService PlayerService { get; }
        IShipFactory ShipFactory { get; }
        #endregion


        public BattleshipService(IPlayerService playerService, IShipFactory shipFactory)
        {
            PlayerService = playerService;
            ShipFactory = shipFactory;
        }

        public string StartTheGame(string[] args)
        {
            //Check if the inputs are valid otherwise throw error
            string[] _input_H_W = Convert.ToString(args[0])?.Trim().Split(' ');
            int width;
            char height;

            //read battle field area
            if (_input_H_W.Length != 2)
            {
                throw new ArgumentException("Input for Height and width of the battle field is not correct, please supply valid inputs.");
            }
            else
            {
                width = Convert.ToInt16(_input_H_W[0]);
                if (!(width > 0 && width < 10))
                    throw new ArgumentException("Invalid width passed as the input.");

                char? _height = Convert.ToString(_input_H_W[1])?.Trim()[0];
                if (!(_height.HasValue && _height >= 'A' && _height <= 'Z'))
                    throw new ArgumentException("Invalid Height passed as the input.");

                height = _height.Value;
            }


            short numberOfShips = Convert.ToInt16(args[1]);
            if (numberOfShips > width * (Convert.ToInt16(height) - 64))
            {
                throw new ArgumentException("Invalid Number Of Ships passed as the input.");
            }

            //READ ship details
            string shipType;
            string _P1_shipCoordinates;
            string _P2_shipCoordinates;
            List<IShip> P1Ships = new List<IShip>();
            List<IShip> P2Ships = new List<IShip>();

            short count;
            for (count = 2; count < numberOfShips + 2; count++)
            {
                string[] _input_type_dimention_coordinates = args[count].Split(' ');
                if (_input_type_dimention_coordinates.Length < 5)
                {
                    throw new ArgumentException("Invalid Type, Dimention & Coordinates Of Ships passed as the input.");
                }
                else
                {
                    shipType = _input_type_dimention_coordinates[0];

                    int.TryParse(_input_type_dimention_coordinates[1], out int _ship_dimention_w);
                    int.TryParse(_input_type_dimention_coordinates[2], out int _ship_dimention_h);

                    if (_ship_dimention_w == 0 || _ship_dimention_h == 0)
                    {
                        throw new ArgumentException("Invalid Dimention Of Ships passed as the input.");
                    }

                    Enum.TryParse(shipType, out ShipType typeOfShip);

                    _P1_shipCoordinates = _input_type_dimention_coordinates[3];
                    var P1Ship = InitializeShip(_ship_dimention_w, _ship_dimention_h, _P1_shipCoordinates, P1Ships, typeOfShip);
                    P1Ships.Add(P1Ship);

                    _P2_shipCoordinates = _input_type_dimention_coordinates[4];
                    var P2Ship = InitializeShip(_ship_dimention_w, _ship_dimention_h, _P2_shipCoordinates, P2Ships, typeOfShip);
                    P2Ships.Add(P2Ship);
                }
            }

            //Read the target details
            string _input_target_p1 = args[count];//P1 Targets
            string _input_target_p2 = args[count + 1];//P2 Targets

            this.BattleshipArea = new Area(width, height);

            PlayerService.CreatePlayer("Player-1", P1Ships.AsEnumerable(), _input_target_p1.Split(' '));
            PlayerService.CreatePlayer("Player-2", P2Ships.AsEnumerable(), _input_target_p2.Split(' '));

            //Start the game
            return PlayerService.Play();
        }

        /// <summary>
        /// Create the new ship object using the ship factory, Coordinates will be setup using H/W
        /// </summary>
        /// <param name="_ship_dimention_w">width of the ship</param>
        /// <param name="_ship_dimention_h">height of the ship</param>
        /// <param name="_shipCoordinates">Position of the ship</param>
        /// <param name="Ships">Ships of the counter player</param>
        /// <param name="typeOfShip">type of ship P or Q</param>
        /// <returns></returns>
        private IShip InitializeShip(int _ship_dimention_w, int _ship_dimention_h, string _shipCoordinates, List<IShip> Ships, ShipType typeOfShip)
        {
            var playerShip = ShipFactory.Create(typeOfShip);
            playerShip.Ship.Coordinates.Add(_shipCoordinates);

            if (_ship_dimention_w > 0 || _ship_dimention_h > 0)
            {
                for (int i = 1; i < _ship_dimention_h; i++)
                {
                    var newdim = ((char)(Convert.ToInt16(_shipCoordinates.ToCharArray()[0]) + 1)).ToString() + _shipCoordinates.ToCharArray()[1].ToString();
                    playerShip.Ship.Coordinates.Add(newdim);
                }
                for (int i = 1; i < _ship_dimention_w; i++)
                {
                    var newdim = (_shipCoordinates.ToCharArray()[0].ToString() + (Convert.ToInt16(_shipCoordinates.ToCharArray()[1].ToString()) + 1).ToString());
                    playerShip.Ship.Coordinates.Add(newdim);
                }
            }
            return playerShip;
        }
    }
}
