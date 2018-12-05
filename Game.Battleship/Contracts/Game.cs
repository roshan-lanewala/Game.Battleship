using Game.Battleship.Extensions;
using System;

namespace Game.Battleship.Contracts
{
    public class Game
    {
        private const int NUMBER_OF_SHIPS_PER_GAME = 5;
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Game(int boardHeight, int boardWidth)
        {
            Player1 = new Player("Alexa", boardHeight, boardWidth)
                .BuildPlayerShips(NUMBER_OF_SHIPS_PER_GAME);
            Player2 = new Player("Siri", boardHeight, boardWidth)
                .BuildPlayerShips(NUMBER_OF_SHIPS_PER_GAME);
        }

        public string Play()
        {
            while (!Player1.HasLost && !Player2.HasLost)
            {
                PlayRound();
            }

            if (Player1.HasLost)
                return $"{Player2.Name} has won the game!";

            return $"{Player1.Name} has won the game!";
        }

        private void PlayRound()
        {
            var coordinates = Player1.FireShot();
            var result = Player2.ProcessShot(coordinates);
            Player1.ProcessShotResult(coordinates, result);

            if(!Player2.HasLost)
            {
                coordinates = Player2.FireShot();
                result = Player1.ProcessShot(coordinates);
                Player2.ProcessShotResult(coordinates, result);
            }
        }
    }
}
