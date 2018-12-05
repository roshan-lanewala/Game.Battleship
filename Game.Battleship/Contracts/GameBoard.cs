using System.Collections.Generic;

namespace Game.Battleship.Contracts
{
    public class GameBoard
    {
        public List<Panel> Panels { get; set; }
        public int BoardHeight { get; private set; }
        public int BoardWidth { get; private set; }

        public GameBoard(int boardHeight, int boardWidth)
        {
            Panels = new List<Panel>();
            BoardHeight = boardHeight;
            BoardWidth = boardWidth;
            this.CreateBoard();
        }

        public void CreateBoard()
        {
            for (int i = 0; i < BoardHeight; i++)
            {
                for (int j = 0; j < BoardWidth; j++)
                {
                    Panels.Add(new Panel(i, j));
                }
            }
        }
    }
}
