using System;
using System.Collections.Generic;
using System.Text;

namespace Convex_Hull_Library
{
    public enum Turn
    {
        Collinear,
        Counterclockwise,
        Clockwise
    }

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
        /// Finds the point with maximum x coordinate. If multiple points of these points exist, returns the one with minimum y coordinate. 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2D RightMostPoint(List<Point2D> points)
        {
            if (points == null || points.Count == 0)
            {
                return null;
            }

            Point2D result = points[0];

            for (int i = 1; i < points.Count; i++)
            {
                Point2D point = points[i];

                // higher x value, or same x value and lower y value
                if (point.X > result.X || (point.X == result.X && point.Y < result.Y))
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
        /// check whether point Q is left of the line through A and B
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="Q"></param>
        /// <returns></returns>
        public static bool IsPointLeftToLine(Point2D A, Point2D B, Point2D Q)
        {
            // use cross-product's Z value: (Vector1.X * Vector2.Y) - (Vector1.Y * Vector2.X)
            Point2D v1 = new Point2D(B.X - A.X, B.Y - A.Y);

            Point2D v2 = new Point2D(Q.X - A.X, Q.Y - A.Y);

            float z = (v1.X * v2.Y) - (v1.Y * v2.X);

            // point is left to the line through endpoint1 and enpoint2 if z is greater than 0
            return z > 0;
        }

        /// <summary>
        /// check whether point Q is right of the line through A and B
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="Q"></param>
        /// <returns></returns>
        public static bool IsPointRightToLine(Point2D A, Point2D B, Point2D Q)
        {
            // use cross-product's Z value: (Vector1.X * Vector2.Y) - (Vector1.Y * Vector2.X)
            Point2D v1 = new Point2D(B.X - A.X, B.Y - A.Y);

            Point2D v2 = new Point2D(Q.X - A.X, Q.Y - A.Y);

            float z = (v1.X * v2.Y) - (v1.Y * v2.X);

            // point is right to the line through endpoint1 and enpoint2 if z is smaller than 0
            return z < 0;
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

        /// <summary>
        /// compute the polar angle for a given point relative to a given center point
        /// </summary>
        /// <param name="center"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double PolarAngle(Point2D center, Point2D point)
        {
            Point2D p = new Point2D(point.X - center.X, point.Y - center.Y);

            return Math.Atan2(p.Y, p.X);
        }

        /// <summary>
        /// for a line segment p1 - p2, computes whether p2 - p3 is a clockwise, counter-clockwise or collinear turn
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static Turn ComputeTurn(Point2D p1, Point2D p2, Point2D p3)
        {
            // z coordinate of the cross-product of the two vectors p1p2 and p1p3
            float z = (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);

            if (z == 0)
            {
                return Turn.Collinear;
            }
            else if (z > 0)
            {
                return Turn.Counterclockwise;
            }
            else
            {
                return Turn.Clockwise;
            }
        }

        /// <summary>
        /// compute the distance from a point to a line segment AB
        /// </summary>
        /// <param name="A">endpoint A of the line segment</param>
        /// <param name="B">endpoint B of the line segment</param>
        /// <param name="Q">point Q</param>
        /// <returns></returns>
        public static double DistancePointToLineSegment(Point2D A, Point2D B, Point2D Q)
        {
            Vector2D AQ = new Vector2D(A, Q);
            Vector2D AB = new Vector2D(A, B);

            // find T such that A + T * AB is the projection of point Q on the line through A and B
            float T = Vector2D.DotProduct(AQ, AB) / Vector2D.DotProduct(AB, AB);

            if (T < 0)
            {
                // endpoint A is closest to Q
                return Distance(A, Q);
            }
            else if (T > 1)
            {
                // endpoint B is closest to Q
                return Distance(B, Q);
            }
            else
            {
                // point on AB: A + T * AB
                Point2D AB_ = new Point2D(A.X + T * AB.X, A.Y + T * AB.Y);

                return Distance(AB_, Q);
            }
        }
    }
}
