namespace QuoridorCmd.Model
{
    class Player
    {
        public string Name { get; }

        public PlayerColor Color { get; }

        public Position Position { get; set; }

        public int WinningPositionY { get; }

        public int WallCount { get; set; }

        public Player(string name, PlayerColor color)
        {
            Name = name;
            Color = color;
            if (color == PlayerColor.White)
            {
                Position = new Position(5, 9);
                WinningPositionY = 1;
            }
            else
            {
                Position = new Position(5, 1);
                WinningPositionY = 9;
            }
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
