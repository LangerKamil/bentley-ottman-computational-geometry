using System;
using System.Windows;

namespace GeometriaObliczeniowa.Common.BaseClasses
{
    public class Line
    {
        #region Fields
        private readonly Lazy<double> slope;
        #endregion

        #region Properties
        public Point Left { get; private set; }

        public Point Right { get; private set; }

        public bool IsVertical => Left.X == Right.X;

        public bool IsHorizontal => Left.Y == Right.Y;

        public double Slope => slope.Value;
        #endregion

        #region Constructors
        private Line()
        {
            slope = new Lazy<double>(() => calcSlope());
        }

        internal Line(Point start, Point end, double tolerance)
            : this()
        {
            if (start.X < end.X)
            {
                Left = start;
                Right = end;
            }
            else if (start.X > end.X)
            {
                Left = end;
                Right = start;
            }
            else
            {
                //use Y
                if (start.Y < end.Y)
                {
                    Left = start;
                    Right = end;
                }
                else
                {
                    Left = end;
                    Right = start;
                }
            }
        }

        public Line(Point start, Point end, int precision = 5)
            : this(start, end, Math.Round(Math.Pow(0.1, precision), precision)) { }
        #endregion

        #region Methods
        private double calcSlope()
        {
            Point left = Left, right = Right;

            //vertical line has infinite slope
            if (left.Y == right.Y)
            {
                return double.MaxValue;
            }

            return ((right.Y - left.Y) / (right.X - left.X));
        }
        #endregion
    }
}
