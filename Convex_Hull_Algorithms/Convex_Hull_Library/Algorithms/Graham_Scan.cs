using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convex_Hull_Library.Algorithms
{
    public static class Graham_Scan
    {
        private enum Turn
        {
            Collinear,
            Counterclockwise,
            Clockwise
        }

        public static List<Point2D> ComputeConvexHull(List<Point2D> points)
        {
            // TODO null checks and point sets with less than 3 points
            if (points == null || points.Count < 3)
            {
                throw new NotImplementedException();
            }

            List<Point2D> stack = new List<Point2D>();

            Point2D lowestPoint = Point2D.LowestPoint(points);

            points.Remove(lowestPoint);

            stack.Add(lowestPoint);

            points = points.Select(p => 
                new { 
                    Point = p, 
                    PolarAngle = PolarAngle(lowestPoint, p), 
                    Distance = Point2D.Distance(lowestPoint, p) })
                .GroupBy(p => p.PolarAngle)
                .Select(p => p.OrderByDescending(q => q.Distance).First())
                .OrderBy(p => p.PolarAngle)
                .Select(p => p.Point)
                .ToList();

            foreach (Point2D point in points)
            {
                while (stack.Count > 1)
                {
                    Turn turn =  CounterClockWise(stack[stack.Count - 2], stack[stack.Count - 1], point);

                    if (turn == Turn.Collinear || turn == Turn.Clockwise)
                    {
                        stack.RemoveAt(stack.Count - 1);
                    }
                    else
                    {
                        break;
                    }
                }

                stack.Add(point);
            }

            stack.Reverse();

            return stack;
        }

        private static double PolarAngle(Point2D center, Point2D point)
        {
            Point2D p = new Point2D(point.X - center.X, point.Y - center.Y);

            return Math.Atan2(p.Y, p.X);
        }

        private static Turn CounterClockWise(Point2D p1, Point2D p2, Point2D p3)
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
    }
}
