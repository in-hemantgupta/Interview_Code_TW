
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
    public class ShipFactoryTests
    {
        public List<Player> Players { get; set; }
        private IBattleshipService BattleshipService { get; set; }

        private Mock<IPlayerService> PlayerService { get; set; }
        private Mock<IShipFactory> ShipFactory { get; set; }

        string[] args = {
                "5 E",
                "2",
                "Q 1 1 A1 B2",
                "P 2 1 D4 C3",
                "A1 B2 B2 B3",
                "A1 B2 B3 A1 D1 E1 D4 D4 D5 D5"};

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

            player = new Mock<Player>(It.IsAny<string>()).Object;
            IShip intShip = new ShipP();
            intShip.Ship = shipMock;

            var shipFactory = new Mock<IShipFactory>();
            shipFactory.Setup(s => s.Create(It.IsAny<Constraints.ShipType>())).Returns(() => intShip).Callback(() => { intShip = iShip.Value; });
            this.ShipFactory = shipFactory;
        }

        [Test()]
        public void Create_WhenCalled_With_P_ShouldCreateShip()
        {
            //Act
            var Iship = ShipFactory.Object.Create(Constraints.ShipType.P);
            //assert
            Assert.IsNotNull(Iship.Ship);
        }

        [Test()]
        public void Create_WhenCalled_With_P_ShouldNotBeInDestroyedState()
        {
            //Act
            var Iship = ShipFactory.Object.Create(Constraints.ShipType.P);
            //assert
            Assert.AreEqual(Iship.Ship.IsDestroyed, false);
        }

    }
}
