using System;
using System.Collections.Generic;

namespace Game.Battleship.Contracts
{
    public class Ship
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Hits { get; set; }
        public List<Guid> PanelIds { get; set; }
        public bool IsSunk
        {
            get
            {
                return Hits >= Width;
            }
        }

        public Ship()
        {
            PanelIds = new List<Guid>();
        }
    }
}
