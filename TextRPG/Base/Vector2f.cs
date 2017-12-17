using System;

namespace TextRPG
{
    public struct Vector2f
    {
        public static Vector2f Zero = new Vector2f(0, 0);
        public static Vector2f One =  new Vector2f(1, 1);
        public static Vector2f Right = new Vector2f(1, 0);
        public static Vector2f Left = new Vector2f(-1, 0);
        public static Vector2f Up = new Vector2f(0, 1);
        public static Vector2f Down = new Vector2f(0, -1);

        public static Vector2f Center = new Vector2f(0.5f, 0.5f);

        public float X { get; set; }
        public float Y { get; set; }

        public Vector2f(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public double Distance(Vector2f other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public static Vector2f operator+(Vector2f a, Vector2f b)
        {
            return new Vector2f
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static Vector2f operator-(Vector2f a, Vector2f b)
        {
            return new Vector2f
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static Vector2f operator*(Vector2f a, float scalar)
        {
            return new Vector2f
            {
                X = a.X * scalar,
                Y = a.Y * scalar
            };
        }
    }
}