using Battleship.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Battleship.Models;
using Moq;

namespace Battleship.Services.Tests
{
    [TestFixture()]
    public class PlayerServiceTests
    {
        public List<Player> Players { get; set; }
        private Mock<IPlayerService> PlayerService { get; set; }
        private Mock<IShipFactory> ShipFactory { get; set; }

        List<IShip> ships;
        Lazy<Ship> ship;
        Player player;

        [SetUp]
        public void Setup()
        {
            var mShip = new Mock<Ship>(It.IsAny<Constraints.ShipType>());

            mShip.Object.Coordinates = new List<Coordinates>();
            ship = new Lazy<Ship>(() => mShip.Object);

            var miShip = new Mock<IShip>();
            miShip.Setup(s => s.Attack(It.IsAny<string>(), It.IsAny<IMissile>())).Returns(It.IsAny<bool>);

            Ship shipMock = new Ship(It.IsAny<Constraints.ShipType>());
            shipMock.Coordinates = new List<Coordinates>();

            miShip.Setup(s => s.Ship).Returns(() => shipMock).Callback(() => { shipMock = ship.Value; });

            var iShip = new Lazy<IShip>(() => miShip.Object);

            ships = new List<IShip>() { iShip.Value, iShip.Value };

            var playerService = new Mock<IPlayerService>();
            Players = new List<Player>();

            playerService.SetupGet(p => p.Players).Returns(Players);
            playerService.Setup(p => p.CreatePlayer(It.IsAny<string>(), It.IsAny<IEnumerable<IShip>>(), It.IsAny<string[]>(), It.IsAny<IMissile>()))
                .Callback((string Name, IEnumerable<IShip> paramShips, string[] targets)=>
                {
                    Players.Add(new Player(Name, null)
                    {
                        Ships = paramShips,
                        Targets = targets
                    });
                });

            playerService.Setup(p => p.PlayerTurn(player, It.IsAny<int>(), ships));
            this.PlayerService = playerService;
        }

        [Test()]
        public void CreatePlayer_WhenCalled_ShouldCreate_1_Player()
        {
            //Act
            PlayerService.Object.CreatePlayer("P1", ships, new string[] { "A1", "B1" }, null);
            //assert
            Assert.AreEqual(PlayerService.Object.Players.Count, 1);
        }

    }
}