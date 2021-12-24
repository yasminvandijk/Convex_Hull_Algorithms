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
            Console.WriteLine("Hello World!");

            List<Point2D> points = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(0.5f, 0.5f),
                new Point2D(1, 0)
            };

            //List<Point2D> result = Jarvis_March.ComputeConvexHull(points);
            List<Point2D> result = Graham_Scan.ComputeConvexHull(points);

            result.ForEach(p => Console.WriteLine($"{p.X} {p.Y}"));
        }
    }
}
