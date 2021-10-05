using System.Numerics;

namespace QuoridorConsole
{
    class Wall
    {
        public Vector2 LeftTopPoint { get; }

        public Vector2 RightTopPoint { get; }

        public Vector2 LeftBottomPoint { get; }

        public Vector2 RightBottomPoint { get; }

        public bool IsVertical { get; }

        public Wall(Vector2 leftTopPoint, Vector2 rightTopPoint, Vector2 leftBottomPoint, Vector2 rightBottomPoint, bool isVertical)
        {
            LeftTopPoint = leftTopPoint;
            RightTopPoint = rightTopPoint;
            LeftBottomPoint = leftBottomPoint;
            RightBottomPoint = rightBottomPoint;
            IsVertical = isVertical;
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

                return LeftTopPoint.Equals(w.LeftTopPoint) && RightTopPoint.Equals(w.RightTopPoint)
                    && LeftBottomPoint.Equals(w.LeftBottomPoint) && RightBottomPoint.Equals(w.RightBottomPoint)
                    && IsVertical.Equals(w.IsVertical);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
