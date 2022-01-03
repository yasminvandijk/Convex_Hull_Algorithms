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
            // TODO remove colinear points from the convex hull
            if (points == null || points.Count < 3)
            {
                throw new NotImplementedException();
            }
            
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
                    if (endPoint == pointOnHull || Point2D.IsPointLeftToLine(ConvexHull[i], endPoint, points[j]))
                    {
                        endPoint = points[j];
                    }
                }

                i++;
                pointOnHull = endPoint;
            }

            return ConvexHull;
        }
    }
}
