using GeometriaObliczeniowa.Common.Extensions;
using GeometriaObliczeniowa.Controls.CoordinateSystem;
using GeometriaObliczeniowa.View.MainView;
using System;
using System.Collections.ObjectModel;
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
        private readonly SolidColorBrush blackStroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private Storyboard storyboard;
        #endregion

        #region Properties
        public SegmentsViewModel SegmentsViewModel
        {
            get { return (SegmentsViewModel)GetValue(SegmentsViewModelProperty); }
            set { SetValue(SegmentsViewModelProperty, value); }
        }

        public static readonly DependencyProperty SegmentsViewModelProperty =
            DependencyProperty.Register(
                name: nameof(SegmentsViewModel),
                propertyType: typeof(SegmentsViewModel),
                ownerType: typeof(CoordinateSystemControl),
                typeMetadata: new PropertyMetadata(default));



        public ObservableCollection<SegmentViewModel> Segments
        {
            get { return (ObservableCollection<SegmentViewModel>)GetValue(SegmentsProperty); }
            set { SetValue(SegmentsProperty, value); }
        }

        public static readonly DependencyProperty SegmentsProperty =
            DependencyProperty.Register(
                name: nameof(Segments),
                propertyType: typeof(ObservableCollection<SegmentViewModel>),
                ownerType: typeof(CoordinateSystemControl),
                typeMetadata: new PropertyMetadata(default));



        public bool IsSweepRunning
        {
            get { return (bool)GetValue(IsSweepRunningProperty); }
            set { SetValue(IsSweepRunningProperty, value); }
        }

        public static readonly DependencyProperty IsSweepRunningProperty =
            DependencyProperty.Register(
                name: nameof(IsSweepRunning),
                propertyType: typeof(bool),
                ownerType: typeof(CoordinateSystemControl),
                typeMetadata: new PropertyMetadata(default));
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
        public void RenderCoordinateSystem()
        {
            this.DrawCoordinateSystem();

            if (this.Segments != null)
            {
                this.Segments.ForEach(this.DrawSegment);
            }
        }

        public void RunSweep()
        {
            if (this.IsSweepRunning)
            {
                this.AnimateSweep();
            }
            else if (this.SegmentsViewModel.Segments.Any())
            {
                this.RenderCoordinateSystem();
            }
            else
            {
                this.DrawCoordinateSystem();
            }

            this.IsSweepRunning = false;
        }

        private void InitializeProperties()
        {
            this.coordinateSystem = new CoordinateSystemElements();
            this.IsSweepRunning = false;
            //this.SegmentsViewModel = new SegmentsViewModel();
        }

        private void DrawSegment(SegmentViewModel segmentsViewModel)
        {
            Line line = new Line();
            line.StrokeThickness = 2;
            line.Stroke = blackStroke;
            line.X1 = segmentsViewModel.StartingPoint.X;
            line.Y1 = segmentsViewModel.StartingPoint.Y;
            line.X2 = segmentsViewModel.EndingPoint.X;
            line.Y2 = segmentsViewModel.EndingPoint.Y;

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

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(SegmentsViewModel.Segments))
            {
                if (this.SegmentsViewModel != null)
                {
                    this.RenderCoordinateSystem();
                }
            }

            if (e.Property.Name == nameof(IsSweepRunning))
            {
                this.RunSweep();
            }
            base.OnPropertyChanged(e);
        }
        #endregion
    }
}
