using System;

namespace Game.Battleship.Contracts
{
    public class Panel
    {
        public Guid PanelId { get; private set; }
        public PanelType PanelType { get; set; }
        public Coordinates Coordinates { get; set; }

        public Panel(int row, int column)
        {
            PanelId = Guid.NewGuid();
            Coordinates = new Coordinates(row, column);
            PanelType = PanelType.Empty;
        }

        public string Status
        {
            get
            {
                return Enum.GetName(typeof(PanelType), PanelType);
            }
        }

        public bool IsOccupied
        {
            get
            {
                return PanelType == PanelType.Occupied;
            }
        }

        public bool IsRandomAvailable
        {
            get
            {
                return (Coordinates.Row % 2 == 0 && Coordinates.Column % 2 == 0)
                    || (Coordinates.Row % 2 == 1 && Coordinates.Column % 2 == 1);
            }
        }
    }
}
