using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem
{
    public sealed class CoordinateSystemElements
    {
        #region Properties
        public Tuple<Point, Point> YAxis { get; set; }

        public Tuple<Point, Point> XAxis { get; set; }

        public SolidColorBrush Stroke { get; set; }

        public double StrokeThickness { get; set; }
        #endregion

        #region Constructors
        public CoordinateSystemElements()
        {
            this.InitializeProperties();
        }
        #endregion

        #region Methods
        private void InitializeProperties()
        {
            this.YAxis = new Tuple<Point, Point>(new Point(0, 180), new Point(0, -180));
            this.XAxis = new Tuple<Point, Point>(new Point(180, 0), new Point(-180, 0));
            this.Stroke = new SolidColorBrush(Color.FromRgb(102, 194, 255));
            this.StrokeThickness = 1;
        }

        public List<Line> GenerateCoordinateSystem()
        {
            List<Line> lines = new List<Line>()
            {
                new Line()
                {
                    StrokeThickness = StrokeThickness,
                    X1 = YAxis.Item1.X,
                    Y1 = YAxis.Item1.Y,
                    Stroke = Stroke
                },
                new Line()
                {
                    StrokeThickness = StrokeThickness,
                    X1 = YAxis.Item2.X,
                    Y1 = YAxis.Item2.Y,
                    Stroke = Stroke
                },
                new Line()
                {
                    StrokeThickness = StrokeThickness,
                    X1 = XAxis.Item1.X,
                    Y1 = XAxis.Item1.Y,
                    Stroke = Stroke
                },
                new Line()
                {
                    StrokeThickness = StrokeThickness,
                    X1 = XAxis.Item2.X,
                    Y1 = XAxis.Item2.Y,
                    Stroke = Stroke
                },
            };
            return lines;
        }
        #endregion
    }
}
