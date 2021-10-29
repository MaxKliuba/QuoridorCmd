using System;

namespace QuoridorCmd.Model
{
    class Wall
    {
        private static readonly char[] chars = { '_', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', };

        public Position LeftTopPosition { get; }

        public bool IsVertical { get; }

        public Wall(Position leftTopPosition, bool isVertical)
        {
            LeftTopPosition = leftTopPosition;
            IsVertical = isVertical;
        }

        public Wall(string code)
        {
            LeftTopPosition = new Position(Array.IndexOf(chars, code[0]), code[1] - '0');
            IsVertical = code[2].Equals('v');
        }

        public string GetCode()
        {
            return $"{chars[(int)LeftTopPosition.Coordinate.X]}{LeftTopPosition.Coordinate.Y}{GetWallType()}";
        }

        private char GetWallType()
        {
            return IsVertical ? 'v' : 'h';
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Wall w = (Wall)obj;

                return LeftTopPosition.Equals(w.LeftTopPosition) && IsVertical.Equals(w.IsVertical);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
