using System;
using System.Numerics;

namespace QuoridorCmd.Model
{
    class Position
    {
        private static readonly char[] chars = { '_', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', };

        public Vector2 Coordinate { get; private set; }

        public string Code { get; private set; }

        public Position(float x, float y) : this(new Vector2(x, y))
        {

        }

        public Position(Vector2 coordinate)
        {
            Coordinate = coordinate;
            Code = FromCoordinateToCode(coordinate);
        }

        public Position(string code)
        {
            Coordinate = FromCodeToCoordinate(code);
            Code = code;
        }

        public void SetCoordinate(float x, float y)
        {
            SetCoordinate(new Vector2(x, y));
        }

        public void SetCoordinate(Vector2 coordinate)
        {
            Coordinate = coordinate;
            Code = FromCoordinateToCode(coordinate);
        }

        public void SetCode(string code)
        {
            Coordinate = FromCodeToCoordinate(code);
            Code = code;
        }

        public static string FromCoordinateToCode(Vector2 coordinate)
        {
            char ch = '_';

            if((int)coordinate.X > 0 && (int)coordinate.X < chars.Length)
            {
                ch = chars[(int)coordinate.X];
            }

            return $"{ch}{coordinate.Y}";
        }

        public static Vector2 FromCodeToCoordinate(string code)
        {

            return new Vector2(Array.IndexOf(chars, code[0]), code[1] - '0');
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Position p = (Position)obj;

                return Coordinate.Equals(p.Coordinate) || Code.Equals(p.Code);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
