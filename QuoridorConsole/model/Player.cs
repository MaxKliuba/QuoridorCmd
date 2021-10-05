using System.Numerics;

namespace QuoridorConsole
{
    class Player
    {
        public string Name { get; }

        public Vector2 Position { get; set; }

        public int WinningPositionY { get; }

        public int WallCount { get; set; }

        public Player(string name, Vector2 position, int winningPositionY)
        {
            Name = name;
            Position = position;
            WinningPositionY = winningPositionY;
            WallCount = 10;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Player p = (Player)obj;

                return Name.Equals(p.Name) && Position.Equals(p.Position)
                    && WinningPositionY.Equals(p.WinningPositionY) && WallCount.Equals(p.WallCount);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
