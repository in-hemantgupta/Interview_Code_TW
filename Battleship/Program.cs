using Battleship.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using static Battleship.Models.Constraints;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             Inputs:
            - 1. The first line of the input contains the width and height of the battle area respectively.
            - 2. The second line of the input contains the number of battleships that each player gets (N).
            - 3. Read N lines where each line contains the type of the battleship, its dimensions (width and height) 
                    and coordinates of ship for Player-1 and Player-2.
            - 4. The second last line contains the sequence of the target locations of missiles fired by Player-1.
            - 5. The last line contains the sequence of the target locations of missiles fired by Player-2.            
            
            //args = new string[] {
            //    "5 E",
            //    "2",
            //    "Q 1 1 A1 B2",
            //    "P 2 1 D4 C3",
            //    "A1 B2 B2 B3",
            //    "A1 B2 B3 A1 D1 E1 D4 D4 D5 D5"
            //};

             * */

            //Register Interfaces using UNITY
            var container = new UnityContainer();

            container.RegisterType<IPlayerService, PlayerService>();

            //Register Ship Factory
            container.RegisterType<IShip, ShipP>(ShipType.P.ToString());
            container.RegisterType<IShip, ShipQ>(ShipType.Q.ToString());
            IShip shipFactory(ShipType shipType) => container.Resolve<IShip>(shipType.ToString());
            var factory = new ShipFactory(shipFactory);
            container.RegisterInstance<IShipFactory>(factory);

            container.RegisterType<IBattleshipService, BattleshipService>();

            //Resolve the service and start the game
            var battleshipService = container.Resolve<IBattleshipService>();
            string message = battleshipService.StartTheGame(args);
            Console.Read();
        }

    }
}
