using GeometriaObliczeniowa.Models;
using GeometriaObliczeniowa.View;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeometriaObliczeniowa.Controls
{
    /// <summary>
    /// Interaction logic for CoordinateSystemControl.xaml
    /// </summary>
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
            this.coordinateSystemLines.ForEach(c => DrawCoordinateSystem(c));
        }
        #endregion

        #region Methods
        private void InitializeProperties()
        {
            this.coordinateSystemLines = new List<Segment>
                {
                    new Segment
                    {
                        X1 = 0,
                        Y1 = 200,
                        X2 = 0,
                        Y2 = -200
                    },
                    new Segment
                    {
                        X1 = 200,
                        Y1 = 0,
                        X2 = -200,
                        Y2 = 0
                    }
                };
        }

        private void DrawCoordinateSystem(Segment segment)
        {
            var stroke = new SolidColorBrush();
            stroke.Color = Color.FromRgb(102, 194, 255);

            Line line1 = new Line();
            line1.StrokeThickness = 1;
            line1.X1 = segment.X1;
            line1.Y1 = segment.Y1;
            line1.Stroke = stroke;
            this.coordinateSystem.Children.Add(line1);

            Line line2 = new Line();
            line2.StrokeThickness = 1;
            line2.X1 = segment.X2;
            line2.Y1 = segment.Y2;
            line2.Stroke = stroke;
            this.coordinateSystem.Children.Add(line2);
        }

        public void Draw(SegmentsViewModel segment)
        {
            var stroke = new SolidColorBrush();
            stroke.Color = Color.FromRgb(0, 0, 0);

            Line line = new Line();
            line.StrokeThickness = 2;
            line.Stroke = stroke;

            line.X1 = segment.X1;
            line.Y1 = segment.Y1;
            line.X2 = segment.X2;
            line.Y2 = segment.Y2;

            this.coordinateSystem.Children.Add(line);
        }
        #endregion
    }
}
