using Game.Battleship.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Game.Battleship.Contracts
{
    public class FiringBoard : GameBoard
    {
        public FiringBoard(int boardHeight, int boardWidth)
            : base(boardHeight, boardWidth)
        {
        }

        public List<Coordinates> GetOpenRandomPanels()
        {
            return Panels.Where(w => w.PanelType == PanelType.Empty && w.IsRandomAvailable)
                .Select(s => s.Coordinates).ToList();
        }

        public List<Coordinates> GetHitNeighbours()
        {
            List<Panel> panels = new List<Panel>();
            var hits = Panels.Where(w => w.PanelType == PanelType.Hit).ToList();
            hits.ForEach(hit => panels.AddRange(GetNeighbours(hit.Coordinates).ToList()));
            return panels.Distinct().Where(w => w.PanelType == PanelType.Empty)
                .Select(s => s.Coordinates).ToList();
        }

        private List<Panel> GetNeighbours(Coordinates coordinates)
        {
            var row = coordinates.Row;
            var column = coordinates.Column;
            List<Panel> panels = new List<Panel>();
            if(column > 1)
            {
                panels.Add(panels.At(row, column - 1));
            }
            if(row > 1)
            {
                panels.Add(panels.At(row - 1, column));
            }
            if(row < 10)
            {
                panels.Add(panels.At(row + 1, column));
            }
            if (column < 10)
            {
                panels.Add(panels.At(row, column + 1));
            }
            return panels;
        }
    }
}
