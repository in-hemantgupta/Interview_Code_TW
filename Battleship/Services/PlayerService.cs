using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship.Services
{
    public class PlayerService : IPlayerService
    {
        public List<Player> Players { get; set; }
        readonly ManualResetEvent mres2 = new ManualResetEvent(false);
        public PlayerService()
        {
            this.Players = new List<Player>();
        }

        public void CreatePlayer(string Name, IEnumerable<IShip> ships, string[] targets, IMissile missile)
        {
            this.Players.Add(new Player(Name, missile)
            {
                Ships = ships,
                Targets = targets
            });
        }

        /// <summary>
        /// Start the game, player will get his chance until he/she miss the target and once missed chance will goes to other player
        /// </summary>
        public string Play()
        {
            int maxCount = this.Players.Max(p => p.Targets.Length);
            List<Task> tasks = new List<Task>();
            var p1 = Players.First(p => p.Name == "Player-1");
            var p2 = Players.First(p => p.Name == "Player-2");

            //Start a thread that will play the player 1
            Task task = Task.Factory.StartNew(() =>
            {
                PlayerTurn(p1, maxCount, p2.Ships);
            });
            tasks.Add(task);
            //Start a thread that will play the player 2
            Task task1 = Task.Factory.StartNew(() =>
            {
                PlayerTurn(p2, maxCount, p1.Ships);
            });
            tasks.Add(task1);

            tasks.ForEach(t => t.Wait());

            //get the player who lost all the ships
            var loosingPlayer = Players.FirstOrDefault(p => p.Ships.All(s => s.Ship.IsDestroyed));

            string returnMessage = "";
            //other player won
            if (loosingPlayer != null)
                returnMessage = (Players.First(p => p.Name != loosingPlayer.Name).Name + " won the battle");
            else
                returnMessage = ("None of the player was able to destroy all the ships of other player, hence game is draw.");
            Console.WriteLine(returnMessage);
            return returnMessage;
        }

        public void PlayerTurn(Player player, int maxTargets, IEnumerable<IShip> targetShips)
        {
            int pCount = 0;

            for (pCount = 0; pCount < maxTargets; pCount++)
            {
                if (pCount >= player.Targets.Length)
                {
                    Console.WriteLine(player.Name + " has no more missiles left to launch");
                    Thread.Sleep(200);
                    //chance goes to other player
                    if (mres2.Set())
                    {
                        mres2.WaitOne();
                    }
                }
                else
                {

                    bool hit = targetShips.Any(t => t.Attack(player.Targets[pCount], player.Missile));
                    var pname = player.Name;
                    Console.WriteLine(player.Name + " fires a missile with target " + player.Targets[pCount] + " which got " + (hit ? "hit" : "missed"));
                    if (!hit)
                    {
                        Thread.Sleep(200);
                        //chance goes to other player
                        if (!mres2.Set())
                        {
                            mres2.WaitOne();
                        }
                    }
                }
            }
        }
    }
}
