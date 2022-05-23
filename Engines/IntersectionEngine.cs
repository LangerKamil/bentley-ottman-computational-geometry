using GeometriaObliczeniowa.Engines.Interface;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace GeometriaObliczeniowa.Engines
{
    public sealed class IntersectionEngine : IIntersectionEngine
    {
        public Point Intersection(IntersectionEngineInput input)
        {
            double A1 = input.Line1End.Y - input.Line1Start.Y;
            double B1 = input.Line1Start.X - input.Line1End.X;
            double C1 = A1 * input.Line1Start.X + B1 * input.Line1Start.Y;

            double A2 = input.Line2End.Y - input.Line2Start.Y;
            double B2 = input.Line2Start.X - input.Line2End.X;
            double C2 = A2 * input.Line2Start.X + B2 * input.Line2Start.Y;

            double determinant = A1 * B2 - A2 * B1;

            if (determinant == 0)
            {
                throw new ArgumentException("Lines are parallel");
            }

            double X = (B2 * C1 - B1 * C2) / determinant;
            double Y = (A1 * C2 - A2 * C1) / determinant;

            return new Point(X, Y);
        }
    }

    public class IntersectionEngineInput
    {
        public Point Line1Start { get; set; }
        public Point Line1End { get; set; }
        public Point Line2Start { get; set; }
        public Point Line2End { get; set; }

        public IntersectionEngineInput(List<Point> coords)
        {
            Line1Start = coords[0];
            Line1End = coords[1];
            Line2Start = coords[2];
            Line2End = coords[3];
        }
    }
}