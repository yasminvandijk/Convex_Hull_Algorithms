using Convex_Hull_Library;
using Convex_Hull_Library.Algorithms;
using System;
using System.Collections.Generic;

namespace Convex_Hull_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point2D> points = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(0.5f, 0.5f),
                new Point2D(0.7f, 0.1f),
                new Point2D(0.7f, 0.3f),
                new Point2D(1, 0)
            };

            {
                Console.WriteLine("Jarvis March");
                List<Point2D> result = Jarvis_March.ComputeConvexHull(points);
                result.ForEach(p => Console.WriteLine($"{p.X} {p.Y}"));
                Console.WriteLine();
            }
            {
                Console.WriteLine("Graham scan");
                List<Point2D> result = Graham_Scan.ComputeConvexHull(points);
                result.ForEach(p => Console.WriteLine($"{p.X} {p.Y}"));
                Console.WriteLine();
            }
            {
                Console.WriteLine("Quickhull");
                List<Point2D> result = Quickhull.ComputeConvexHull(points);
                result.ForEach(p => Console.WriteLine($"{p.X} {p.Y}"));
                Console.WriteLine();
            }
        }
    }
}
