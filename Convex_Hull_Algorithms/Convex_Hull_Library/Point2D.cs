using System;
using System.Collections.Generic;
using System.Text;

namespace Convex_Hull_Library
{
    public class Point2D
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
        
        public Point2D(float x, float y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Finds the point with minimum x coordinate. If multiple points of these points exist, returns the one with minimum y coordinate. 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2D LeftMostPoint(List<Point2D> points)
        {
            if (points == null || points.Count == 0)
            {
                return null;
            }

            Point2D result = points[0];

            for (int i = 1; i < points.Count; i++)
            {
                Point2D point = points[i];

                if (point.X < result.X && point.Y < result.Y)
                {
                    result = point;
                }
            }

            return result;
        }
    }
}
