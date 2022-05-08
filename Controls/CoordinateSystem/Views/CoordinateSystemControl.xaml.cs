using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GeometriaObliczeniowa.Common.Extensions;
using GeometriaObliczeniowa.Controls.CoordinateSystem.Models;
using GeometriaObliczeniowa.Controls.CoordinateSystem.ViewModels;
using GeometriaObliczeniowa.ViewModels;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem.Views
{
    public partial class CoordinateSystemControl : UserControl
    {
        #region Fields
        private CoordinateSystemElements coordinateSystemElements;
        private readonly SolidColorBrush blackStroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private Storyboard storyboard;
        private CoordinateSystemControlViewModel viewModel;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public CoordinateSystemControl()
        {
            this.InitializeComponent();
            //this.InitializeProperties();
            //this.DrawCoordinateSystem();
        }
        #endregion

        #region Methods       
        //public void RenderCoordinateSystem()
        //{
        //    this.DrawCoordinateSystem();

        //    if (this.SegmentsViewModel != null)
        //    {
        //        this.SegmentsViewModel.ForEach(this.DrawSegment);
        //    }
        //}

        //public void RunSweep()
        //{
        //    if (this.IsSweeperRunning)
        //    {
        //        this.AnimateSweep();
        //    }
        //    else if (this.SegmentsViewModel.Any())
        //    {
        //        this.RenderCoordinateSystem();
        //    }
        //    else
        //    {
        //        this.DrawCoordinateSystem();
        //    }

        //    this.IsSweeperRunning = false;
        //}

        //private void InitializeProperties()
        //{
        //    this.coordinateSystemElements = new CoordinateSystemElements();
        //    //this.IsSweeperRunning = false;
        //}


        //private void DrawCoordinateSystem()
        //{
        //    this.coordinateSystemControl.Children.Clear();
        //    this.coordinateSystemElements.GenerateCoordinateSystem().ForEach(c => this.coordinateSystemControl.Children.Add(c));
        //}

        //private void AnimateSweep()
        //{
        //    Line sweeperLine = new Line();
        //    sweeperLine.StrokeThickness = 2;
        //    sweeperLine.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        //    sweeperLine.X1 = -200;
        //    sweeperLine.Y1 = 200;
        //    sweeperLine.X2 = 200;
        //    sweeperLine.Y2 = 200;

        //    DoubleAnimation animation = new DoubleAnimation();
        //    animation.Duration = new Duration(TimeSpan.FromSeconds(10));
        //    animation.From = 200;
        //    animation.To = -200;

        //    DoubleAnimation animation2 = new DoubleAnimation();
        //    animation2.Duration = new Duration(TimeSpan.FromSeconds(10));
        //    animation2.From = 200;
        //    animation2.To = -200;

        //    storyboard = new Storyboard();
        //    storyboard.Children.Add(animation);
        //    storyboard.Children.Add(animation2);
        //    Storyboard.SetTarget(animation, sweeperLine);
        //    Storyboard.SetTarget(animation2, sweeperLine);
        //    Storyboard.SetTargetProperty(animation, new PropertyPath(Line.Y1Property));
        //    Storyboard.SetTargetProperty(animation2, new PropertyPath(Line.Y2Property));

        //    this.coordinateSystemControl.Children.Add(sweeperLine);
        //    storyboard.Begin(this);
        //}

        //protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    if (this.SegmentsViewModel != null)
        //    {
        //        this.RenderCoordinateSystem();
        //    }

        //    if (e.Property.Name == nameof(IsSweeperRunning))
        //    {
        //        this.RunSweep();
        //    }
        //    base.OnPropertyChanged(e);
        //}
        #endregion
    }
}
