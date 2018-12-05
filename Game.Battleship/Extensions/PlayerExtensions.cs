using Game.Battleship.Contracts;
using System;

namespace Game.Battleship.Extensions
{
    public static class PlayerExtensions
    {
        public static Player BuildPlayerShips(this Player player, int numberOfShipsPerGame = 5)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < numberOfShipsPerGame; i++)
            {
                var randWidth = rand.Next(2, 6);
                player.AddShipToBoard(new Ship
                {
                    Name = $"Battleship {i + 1}",
                    Width = randWidth,
                });
            }
            return player;
        }
    }
}
