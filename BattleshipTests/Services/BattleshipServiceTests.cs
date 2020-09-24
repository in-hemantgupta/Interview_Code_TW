using Battleship.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Services.Tests
{
    [TestFixture()]
    public class BattleshipServiceTests
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

            mShip.Object.Coordinates = new List<string>();
            ship = new Lazy<Ship>(() => mShip.Object);

            var miShip = new Mock<IShip>();
            miShip.Setup(s => s.Attack(It.IsAny<string>())).Returns(It.IsAny<bool>);

            Ship shipMock = new Ship(It.IsAny<Constraints.ShipType>());
            shipMock.Coordinates = new List<string>();

            miShip.Setup(s => s.Ship).Returns(() => shipMock).Callback(() => { shipMock = ship.Value; });

            var iShip = new Lazy<IShip>(() => miShip.Object);

            ships = new List<IShip>() { iShip.Value, iShip.Value };

            player = new Mock<Player>(It.IsAny<string>()).Object;
            IShip intShip = new ShipP();
            intShip.Ship = shipMock;

            var shipFactory = new Mock<IShipFactory>();
            shipFactory.Setup(s => s.Create(It.IsAny<Constraints.ShipType>())).Returns(() => intShip).Callback(() => { intShip = iShip.Value; });
            this.ShipFactory = shipFactory;

            var playerService = new Mock<IPlayerService>();
            Players = new List<Player>();

            playerService.SetupGet(p => p.Players).Returns(Players);
            playerService.Setup(p => p.CreatePlayer(It.IsAny<string>(), It.IsAny<IEnumerable<IShip>>(), It.IsAny<string[]>()))
                .Callback((string Name, IEnumerable<IShip> paramShips, string[] targets) =>
                {
                    Players.Add(new Player(Name)
                    {
                        Ships = paramShips,
                        Targets = targets
                    });
                });
            playerService.Setup(p => p.Play()).Returns("Player-2 won the battle");

            playerService.Setup(p => p.PlayerTurn(player, It.IsAny<int>(), ships));
            this.PlayerService = playerService;

            var battleshipService = new BattleshipService(PlayerService.Object, ShipFactory.Object);
            this.BattleshipService = battleshipService;
        }


        [Test()]
        public void StartTheGame_WhenCalled_ShouldReturnPlayer2Won()
        {
            //Act
            var message = BattleshipService.StartTheGame(args);

            //assert
            Assert.AreEqual(message, "Player-2 won the battle");
        }

        [Test()]
        public void StartTheGame_WhenCalled_ShipFactory_Create_ShouldCalled_4_Times()
        {
            //Act
            BattleshipService.StartTheGame(args);

            //assert
            ShipFactory.Verify(p => p.Create(It.IsAny<Constraints.ShipType>()), Times.Exactly(4));
        }

        [Test()]
        public void StartTheGame_WhenCalled_PlayerService_Play_ShouldCalled_1_Times()
        {
            //Act
            BattleshipService.StartTheGame(args);

            //assert
            PlayerService.Verify(p => p.Play(), Times.Exactly(1));
        }

    }
}