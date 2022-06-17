using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Extensions;
using GeometriaObliczeniowa.Engines.Interface;
using GeometriaObliczeniowa.Engines.Models;
using System;
using System.Windows;
using GeometriaObliczeniowa.Common.Resources;

namespace GeometriaObliczeniowa.Engines
{
    public sealed class IntersectionEngine : IIntersectionEngine
    {
        public IntersectionEngineOutput FindIntersection(IntersectionEngineInput input)
        {
            return Calculate(input.LineA, input.LineB, input.CalculateTolerance());
        }

        internal static IntersectionEngineOutput Calculate(Line lineA, Line lineB, double tolerance)
        {
            if (lineA == lineB)
            {
                return new IntersectionEngineOutput(new Point(), Strings.No);
            }

            // SORTOWANE
            if (lineA.Left.X.CompareTo(lineB.Left.X) > 0)
            {
                (lineA, lineB) = (lineB, lineA);
            }
            else if (lineA.Left.X.CompareTo(lineB.Left.X) == 0)
            {
                if (lineA.Left.Y.CompareTo(lineB.Left.Y) > 0)
                {
                    (lineA, lineB) = (lineB, lineA);
                }
            }

            // WSPÓŁRZĘDNE ODCINKA A
            double x1 = lineA.Left.X, y1 = lineA.Left.Y;
            double x2 = lineA.Right.X, y2 = lineA.Right.Y;

            // WSPÓŁRZĘDNE ODCINKA B
            double x3 = lineB.Left.X, y3 = lineB.Left.Y;
            double x4 = lineB.Right.X, y4 = lineB.Right.Y;

            // ODCINKI NACHODZĄCE NA SIEBIE NA OSIACH X,Y
            if ((x1 == x2 && x3 == x4 && x1 == x3) || (y1 == y2 && y3 == y4 && y1 == y3))
            {
                // PIERWSZY PUNKT PRZECIECIA ODCINKÓW ORAZ POCZĄTEK DRUGIEGO ODCINKA
                var firstIntersection = new Point(x3, y3);

                // JEŚLI PUNKT ZNAJDUJE SIĘ MIĘDZY POCZĄTKIEM I KOŃCEM ODCIKÓW 
                // ZWRACA WSPÓŁRZĘDNE WEWNĘTRZNEGO ODCINKA
                if (IsInsideLine(lineA, firstIntersection, tolerance) &&
                    IsInsideLine(lineB, firstIntersection, tolerance))
                {
                    // CZY JEDEN ODCINEK ZNAJDUJE SIĘ WEWNĄTRZ DRUGIEGO
                    if (IsInsideLine(lineA, lineB.Right, tolerance))
                    {
                        return new IntersectionEngineOutput(new Point(x3, y3), Strings.Yes, new Line(new Point(x3, y3), new Point(x4, y4)));
                    }
                    return new IntersectionEngineOutput(new Point(x3, y3), Strings.Yes, new Line(new Point(x3, y3), new Point(x2, y2)));
                }
            }

            double x, y;

            // JEŚLI ODCINEK A JEST POŁOŻONY NA OSI X I OBA ODCINKI NIE SĄ POCHYŁE
            // ZWRACA PUNKT PRZECIĘCIA ZA POMOCĄ WZORU y=mx+b
            if (Math.Abs(x1 - x2) < tolerance)
            {
                // WYLICZANIE POCHYŁEJ ODCINKA B ORAZ PUNKTU PRZECIĘCIA NA OSI Y
                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                x = x1;
                y = c2 + m2 * x1;
            }

            // JEŚLI ODCINEK B JEST POŁOŻONY NA OSI X I OBA ODCINKI NIE SĄ POCHYŁE
            // ZWRACA PUNKT PRZECIĘCIA ZA POMOCĄ WZORU y=mx+b
            else if (Math.Abs(x3 - x4) < tolerance)
            {
                // WYLICZANIE POCHYŁEJ ODCINKA A ORAZ PUNKTU PRZECIĘCIA NA OSI Y
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                x = x3;
                y = c1 + m1 * x3;
            }

            // JEŚLI OBA ODCINKI SĄ POCHYŁE
            else
            {
                // POCHYŁA PIERWSZEGO ODCINKA
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                // POCHYŁA DRUGIEGO ODCINKA
                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                x = (c1 - c2) / (m2 - m1);
                y = c2 + m2 * x;

                // JEŚLI PUNKT ZNAJDUJE SIĘ MIĘDZY POCZĄTKIEM I KOŃCEM POCHYŁYCH ODCIKÓW 
                // ZWRACA WSPÓŁRZĘDNE WEWNĘTRZNEGO ODCINKA
                if (!(Math.Abs(-m1 * x + y - c1) < tolerance
                    && Math.Abs(-m2 * x + y - c2) < tolerance))
                {
                    // CZY JEDEN ODCINEK ZNAJDUJE SIĘ WEWNĄTRZ DRUGIEGO
                    if (IsInsideLine(lineA, lineB.Right, tolerance))
                    {
                        return new IntersectionEngineOutput(new Point(x3, y3), Strings.Yes, new Line(new Point(x3, y3), new Point(x4, y4)));
                    }
                    return new IntersectionEngineOutput(new Point(x3, y3), Strings.Yes, new Line(new Point(x3, y3), new Point(x2, y2)));
                }
            }

            var result = new Point(x, y);

            // JEŚLI PUNKT ZNAJDUJE SIĘ MIĘDZY POCZĄTKIEM I KOŃCEM ODCIKÓW 
            // ZWRACA JEGO WSPÓŁRZĘDNE
            if (IsInsideLine(lineA, result, tolerance) &&
                IsInsideLine(lineB, result, tolerance))
            {
                return new IntersectionEngineOutput(result, Strings.Yes);
            }

            // BRAK PRZECIĘCIA
            return new IntersectionEngineOutput(new Point(), Strings.No);
        }

        /// <summary>
        /// Returns true if given point(x,y) is inside the given line segment.
        /// </summary>
        private static bool IsInsideLine(Line line, Point p, double tolerance)
        {
            double x = p.X, y = p.Y;

            var leftX = line.Left.X;
            var leftY = line.Left.Y;

            var rightX = line.Right.X;
            var rightY = line.Right.Y;

            return (x.IsGreaterThanOrEqual(leftX, tolerance) && x.IsLessThanOrEqual(rightX, tolerance)
                        || x.IsGreaterThanOrEqual(rightX, tolerance) && x.IsLessThanOrEqual(leftX, tolerance))
                   && (y.IsGreaterThanOrEqual(leftY, tolerance) && y.IsLessThanOrEqual(rightY, tolerance)
                        || y.IsGreaterThanOrEqual(rightY, tolerance) && y.IsLessThanOrEqual(leftY, tolerance));
        }

    }
}
