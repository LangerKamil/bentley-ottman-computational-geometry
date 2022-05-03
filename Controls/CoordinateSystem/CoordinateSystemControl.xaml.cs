using System;
using GeometriaObliczeniowa.Controls.CoordinateSystem;
using GeometriaObliczeniowa.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GeometriaObliczeniowa.Controls
{
    public partial class CoordinateSystemControl : UserControl
    {
        #region Fields
        private CoordinateSystemElements coordinateSystem;
        private List<Segment> segments;
        private readonly SolidColorBrush blackStroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private Storyboard storyboard;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public CoordinateSystemControl()
        {
            this.InitializeComponent();
            this.InitializeProperties();
            this.DrawCoordinateSystem();
        }
        #endregion

        #region Methods       
        public void RenderCoordinateSystem(List<Segment> segments)
        {
            this.DrawCoordinateSystem();
            segments.ForEach(this.DrawSegment);
            this.segments = segments;
        }

        public bool RunSweep(bool isRunning)
        {
            if (isRunning)
            {
                this.AnimateSweep();
            }
            else if (this.segments.Any())
            {
                this.RenderCoordinateSystem(segments);
            }
            else
            {
                this.DrawCoordinateSystem();
            }
            return false;
        }

        private void InitializeProperties()
        {
            this.coordinateSystem = new CoordinateSystemElements();
        }

        private void DrawSegment(Segment segment)
        {
            Line line = new Line();
            line.StrokeThickness = 2;
            line.Stroke = blackStroke;
            line.X1 = segment.StartingPoint.X;
            line.Y1 = segment.StartingPoint.Y;
            line.X2 = segment.EndingPoint.X;
            line.Y2 = segment.EndingPoint.Y;

            this.coordinateSystemControl.Children.Add(line);
        }

        private void DrawCoordinateSystem()
        {
            this.coordinateSystemControl.Children.Clear();
            this.coordinateSystem.GenerateCoordinateSystem().ForEach(c => this.coordinateSystemControl.Children.Add(c));
        }

        private void AnimateSweep()
        {
            Line sweeperLine = new Line();
            sweeperLine.StrokeThickness = 2;
            sweeperLine.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            sweeperLine.X1 = -200;
            sweeperLine.Y1 = 200;
            sweeperLine.X2 = 200;
            sweeperLine.Y2 = 200;

            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = new Duration(TimeSpan.FromSeconds(10));
            animation.From = 200;
            animation.To = -200;

            DoubleAnimation animation2 = new DoubleAnimation();
            animation2.Duration = new Duration(TimeSpan.FromSeconds(10));
            animation2.From = 200;
            animation2.To = -200;

            storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Children.Add(animation2);
            Storyboard.SetTarget(animation, sweeperLine);
            Storyboard.SetTarget(animation2, sweeperLine);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Line.Y1Property));
            Storyboard.SetTargetProperty(animation2, new PropertyPath(Line.Y2Property));

            this.coordinateSystemControl.Children.Add(sweeperLine);
            storyboard.Begin(this);

        }
        #endregion
    }
}
