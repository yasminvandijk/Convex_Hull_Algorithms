using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Convex_Hull_Library.Algorithms
{
    public static class Jarvis_March
    {
        public static List<Point2D> ComputeConvexHull(List<Point2D> points)
        {
            // TODO null checks and point sets with less than 3 points
            
            Point2D leftMostPoint = Point2D.LeftMostPoint(points);

            Point2D pointOnHull = leftMostPoint;

            int i = 0;

            List<Point2D> ConvexHull = new List<Point2D>();

            Point2D endPoint = null;

            while (endPoint != leftMostPoint)
            {
                ConvexHull.Add(pointOnHull);
                endPoint = points[0];

                for (int j = 0; j < points.Count; j++)
                {
                    if (endPoint == pointOnHull || IsPointLeftToLine(points[j], ConvexHull[i], endPoint))
                    {
                        endPoint = points[j];
                    }
                }

                i++;
                pointOnHull = endPoint;
            }

            return ConvexHull;
        }

        private static bool IsPointLeftToLine(Point2D point, Point2D endpoint1, Point2D endpoint2)
        {
            // use cross-product's Z value: (Vector1.X * Vector2.Y) - (Vector1.Y * Vector2.X)
            Point2D v1 = new Point2D(endpoint2.X - endpoint1.X, endpoint2.Y - endpoint1.Y);

            Point2D v2 = new Point2D(point.X - endpoint1.X, point.Y - endpoint1.Y);

            float z = (v1.X * v2.Y) - (v1.Y * v2.X);

            // point is left to the line through endpoint1 and enpoint2 if z is greater than 0
            return z > 0;
        }
    }
}
