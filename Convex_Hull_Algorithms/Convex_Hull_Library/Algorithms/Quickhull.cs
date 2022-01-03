using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convex_Hull_Library.Algorithms
{
    public static class Quickhull
    {
        public static List<Point2D> ComputeConvexHull(List<Point2D> points)
        {
            // TODO null checks and point sets with less than 3 points
            if (points == null || points.Count < 3)
            {
                throw new NotImplementedException();
            }

            // leftmost and rightmost point
            Point2D A = Point2D.LeftMostPoint(points);
            Point2D B = Point2D.RightMostPoint(points);

            List<Point2D> ConvexHull = new List<Point2D>() { A, B };

            List<Point2D> _points = points.Where(x => x != A && x != B).ToList();

            // points left of AB
            List<Point2D> S1 = new List<Point2D>();

            // points right of AB
            List<Point2D> S2 = new List<Point2D>();

            foreach (Point2D point in _points)
            {
                if (Point2D.IsPointRightToLine(A, B, point))
                {
                    S1.Add(point);
                }
                else if (Point2D.IsPointRightToLine(B, A, point))
                {
                    S2.Add(point);
                }
            }

            FindHull(ConvexHull, S1, A, B);
            FindHull(ConvexHull, S2, B, A);

            return ConvexHull;
        }

        private static void FindHull(List<Point2D> ConvexHull, List<Point2D> points, Point2D P, Point2D Q)
        {
            if (points == null || points.Count == 0)
            {
                return;
            }

            // find point C furthest away from line segment PQ
            Point2D C = points[0];
            double distance = Point2D.DistancePointToLineSegment(P, Q, C);

            for (int i = 1; i < points.Count; i++)
            {
                Point2D _C = points[i];
                double _distance = Point2D.DistancePointToLineSegment(P, Q, _C);

                if (_distance > distance)
                {
                    C = _C;
                    distance = _distance;
                }
            }

            // add C to convex hull, between P and Q
            ConvexHull.Insert(ConvexHull.IndexOf(Q), C);
            points.Remove(C);

            // find subsets S1 and S2, where S1 are points are points right of PC and S2 are points right of CQ
            List<Point2D> S1 = new List<Point2D>();
            List<Point2D> S2 = new List<Point2D>();

            foreach (Point2D point in points)
            {
                if (Point2D.IsPointRightToLine(P, C, point))
                {
                    S1.Add(point);
                }
                else if (Point2D.IsPointRightToLine(C, Q, point))
                {
                    S2.Add(point);
                }
            }

            FindHull(ConvexHull, S1, P, C);
            FindHull(ConvexHull, S2, C, Q);
        }
    }
}
