using System;
using System.Collections.Generic;
using System.Text;

namespace Convex_Hull_Library
{
    public class Vector2D
    {
        private float _x;
        private float _y;

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }

        public Vector2D(float x, float y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// vector from point A to B
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        public Vector2D(Point2D A, Point2D B)
        {
            _x = B.X - A.X;
            _y = B.Y - A.Y;
        }

        public static float DotProduct(Vector2D A, Vector2D B)
        {
            return A.X * B.X + A.Y * B.Y;
        }
    }
}
