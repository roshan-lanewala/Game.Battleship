using Game.Battleship.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Battleship.Contracts
{
    public class Player
    {
        public string Name { get; set; }
        public GameBoard GameBoard { get; set; }
        public FiringBoard FiringBoard { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }

        public Player(string playerName, int boardHeight, int boardWidth)
        {
            Name = playerName;
            Ships = new List<Ship>();
            GameBoard = new GameBoard(boardHeight, boardWidth);
            FiringBoard = new FiringBoard(boardHeight, boardWidth);
        }

        public Coordinates FireShot()
        {
            var hitNeighbours = FiringBoard.GetHitNeighbours();
            Coordinates coords;
            if(hitNeighbours.Any())
            {
                coords = SearchingShot();
            }
            else
            {
                coords = RandomShot();
            }
            return coords;
        }
        
        public ShotResult ProcessShot(Coordinates coords)
        {
            var panel = GameBoard.Panels.At(coords.Row, coords.Column);
            if(!panel.IsOccupied)
            {
                return ShotResult.Miss;
            }

            var ship = Ships.First(x => x.PanelIds.Any(p => p.Equals(panel.PanelId)));
            ship.Hits++;

            return ShotResult.Hit;
        }

        public void ProcessShotResult(Coordinates coords, ShotResult result)
        {
            var panel = FiringBoard.Panels.At(coords.Row, coords.Column);
            switch (result)
            {
                case ShotResult.Hit:
                    panel.PanelType = PanelType.Hit;
                    break;

                default:
                    panel.PanelType = PanelType.Miss;
                    break;
            }
        }

        private void AddShipToBoard(Ship ship)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int allowedTries = 5;
            while (allowedTries > 0)
            {
                var startColumn = rand.Next(0, GameBoard.BoardWidth);
                var startRow = rand.Next(0, GameBoard.BoardHeight);

                int endRow = startRow, endColumn = startColumn;
                var orientation = rand.Next(1, 101) % 2;

                List<int> panelIndexes = new List<int>();
                if (orientation == 0)
                {
                    for (int i = 1; i < ship.Width; i++)
                    {
                        endRow++;
                    }
                }
                else
                {
                    for (int i = 1; i < ship.Width; i++)
                    {
                        endColumn++;
                    }
                }

                // cannot place battleship outside the board.
                if(endColumn > GameBoard.BoardHeight || endRow > GameBoard.BoardWidth)
                {
                    allowedTries--;
                    continue;
                }
                var panelsToUse = GameBoard.Panels.Range(startRow, startColumn, endRow, endColumn);
                if (panelsToUse.Any(x => x.IsOccupied))
                {
                    allowedTries--;
                    continue;
                }

                foreach (var panel in panelsToUse)
                {
                    panel.PanelType = PanelType.Occupied;
                }
                ship.PanelIds.AddRange(panelsToUse.Select(s => s.PanelId).ToList());
                break;
            }
            // able to find panels to fit the ship on the board.
            if (allowedTries > 0)
                Ships.Add(ship);
        }

        private Coordinates SearchingShot()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var hitNeighbours = FiringBoard.GetHitNeighbours();
            var neighbourIdx = rand.Next(hitNeighbours.Count);
            return hitNeighbours[neighbourIdx];
        }

        private Coordinates RandomShot()
        {
            var availablePanels = FiringBoard.GetOpenRandomPanels();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var panelIdx = rand.Next(availablePanels.Count);
            return availablePanels[panelIdx];
        }
    }
}
