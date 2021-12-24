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

                // lower x value, or same x value and lower y value
                if (point.X < result.X || (point.X == result.X && point.Y < result.Y))
                {
                    result = point;
                }
            }

            return result;
        }

        /// <summary>
        /// Finds the point with minimum y coordinate. If multiple points of these points exist, returns the one with minimum x coordinate. 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2D LowestPoint(List<Point2D> points)
        {
            if (points == null || points.Count == 0)
            {
                return null;
            }

            Point2D result = points[0];

            for (int i = 1; i < points.Count; i++)
            {
                Point2D point = points[i];

                // lower y value, or same y value and lower x value
                if (point.Y < result.Y || (point.Y == result.Y && point.X < result.Y))
                {
                    result = point;
                }
            }

            return result;
        }

        /// <summary>
        /// get the distance between two points
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public static double Distance(Point2D point1, Point2D point2)
        {
            float a = point2.X - point1.X;
            float b = point2.Y - point1.Y;

            return Math.Sqrt(a * a + b * b);
        }
    }
}
