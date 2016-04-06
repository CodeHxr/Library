using System;

namespace jMath
{
    public class Vector2D
    {
        #region Properties
        public double X { get; set; }
        public double Y { get; set; }
        public double Length
        {
            get
            {
                var length = Math.Sqrt((X * X) + (Y * Y));
                return length;
            }
        }
        #endregion

        #region Constructors 
        public Vector2D() { }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region Operators and Overrides
        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            var v3 = new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
            return v3;
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            var v3 = new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
            return v3;
        }

        public static Vector2D operator *(Vector2D v, double scalar)
        {
            var vScaled = new Vector2D(v.X * scalar, v.Y * scalar);
            return vScaled;
        }

        public static bool operator ==(Vector2D v1, Vector2D v2)
        {
            if (ReferenceEquals(v1, null))
            {
                return ReferenceEquals(v2, null);
            }

            if (ReferenceEquals(v2, null))
                return false;

            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public static bool operator !=(Vector2D v1, Vector2D v2)
        {
            return !(v1 == v2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }

        public override bool Equals(object otherVector)
        {
            if (!(otherVector is Vector2D))
                return false;

            return this == (Vector2D)otherVector;
        }

        #endregion

        #region Functions
        public Vector2D Normalize()
        {
            var scalar = 1 / Length;
            var vNormalized = this * scalar;

            return vNormalized;
        }
        public double DotProduct(Vector2D v2)
        {
            var dotProduct = (X * v2.X) + (Y * v2.Y);

            return dotProduct;
        }
        #endregion
    }
}
