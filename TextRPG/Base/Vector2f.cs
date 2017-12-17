using System;

namespace TextRPG
{
    public class Vector2f
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2f(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2f()
            : this(0, 0)
        {
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
    }
}