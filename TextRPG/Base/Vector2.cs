using System;

namespace TextRPG
{
    public struct Vector2
    {
        public static Vector2 Zero = new Vector2(0, 0);
        public static Vector2 One =  new Vector2(1, 1);
        public static Vector2 Right = new Vector2(1, 0);
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Up = new Vector2(0, 1);
        public static Vector2 Down = new Vector2(0, -1);

        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public double Distance(Vector2 other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public static Vector2 operator+(Vector2 a, Vector2 b)
        {
            return new Vector2
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static Vector2 operator-(Vector2 a, Vector2 b)
        {
            return new Vector2
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static Vector2 operator*(Vector2 a, int scalar)
        {
            return new Vector2
            {
                X = a.X * scalar,
                Y = a.Y * scalar
            };
        }

    }
}