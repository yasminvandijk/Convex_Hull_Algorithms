using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convex_Hull_Library.Algorithms
{
    public static class Graham_Scan
    {
        public static List<Point2D> ComputeConvexHull(List<Point2D> points)
        {
            // TODO null checks and point sets with less than 3 points
            if (points == null || points.Count < 3)
            {
                throw new NotImplementedException();
            }

            List<Point2D> stack = new List<Point2D>();

            Point2D lowestPoint = Point2D.LowestPoint(points);

            stack.Add(lowestPoint);

            List<Point2D> sortedPoints = points.Select(p => 
                new { 
                    Point = p, 
                    PolarAngle = Point2D.PolarAngle(lowestPoint, p), 
                    Distance = Point2D.Distance(lowestPoint, p) })
                .GroupBy(p => p.PolarAngle)
                .Select(p => p.OrderByDescending(q => q.Distance).First())
                .OrderBy(p => p.PolarAngle)
                .Select(p => p.Point)
                .ToList();

            sortedPoints.Remove(lowestPoint);

            foreach (Point2D point in sortedPoints)
            {
                while (stack.Count > 1)
                {
                    Turn turn =  Point2D.ComputeTurn(stack[stack.Count - 2], stack[stack.Count - 1], point);

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
    }
}
