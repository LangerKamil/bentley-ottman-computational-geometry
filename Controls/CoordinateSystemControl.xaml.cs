using GeometriaObliczeniowa.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeometriaObliczeniowa.Controls
{
    public partial class CoordinateSystemControl : UserControl
    {
        #region Fields
        private List<Segment> coordinateSystemLines;
        #endregion

        #region Constructors
        public CoordinateSystemControl()
        {
            InitializeComponent();
            InitializeProperties();
            this.coordinateSystemLines.ForEach(DrawCoordinateSystem);
        }
        #endregion

        #region Methods       
        public void DrawCoordinateSystem(List<Segment> segments)
        {
            this.coordinateSystem.Children.Clear();
            this.coordinateSystemLines.ForEach(this.DrawCoordinateSystem);
            segments.ForEach(this.DrawSegment);
        }

        private void InitializeProperties()
        {
            this.coordinateSystemLines = new List<Segment>
                {
                    new Segment
                    {
                        StartingPoint = new Point(0,200),
                        EndingPoint = new Point(0,-200),
                    },
                    new Segment
                    {
                        StartingPoint = new Point(200,0),
                        EndingPoint = new Point(-200,0),
                    }
                };
        }

        private void DrawCoordinateSystem(Segment segment)
        {
            var stroke = new SolidColorBrush();
            stroke.Color = Color.FromRgb(102, 194, 255);

            Line line1 = new Line();
            line1.StrokeThickness = 1;
            line1.X1 = segment.StartingPoint.X;
            line1.Y1 = segment.StartingPoint.Y;
            line1.Stroke = stroke;
            this.coordinateSystem.Children.Add(line1);

            Line line2 = new Line();
            line2.StrokeThickness = 1;
            line2.X1 = segment.EndingPoint.X;
            line2.Y1 = segment.EndingPoint.Y;
            line2.Stroke = stroke;
            this.coordinateSystem.Children.Add(line2);
        }

        private void DrawSegment(Segment segment)
        {
            var stroke = new SolidColorBrush();
            stroke.Color = Color.FromRgb(0, 0, 0);

            Line line = new Line();
            line.StrokeThickness = 2;
            line.Stroke = stroke;

            line.X1 = segment.StartingPoint.X;
            line.Y1 = segment.StartingPoint.Y;
            line.X2 = segment.EndingPoint.X;
            line.Y2 = segment.EndingPoint.Y;

            this.coordinateSystem.Children.Add(line);
        }
        #endregion
    }
}
