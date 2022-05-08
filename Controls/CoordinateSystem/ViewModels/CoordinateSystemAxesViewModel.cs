using GeometriaObliczeniowa.Common.BaseClasses;
using System;
using System.Windows;
using System.Windows.Media;
using GeometriaObliczeniowa.Controls.CoordinateSystem.Models;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem.ViewModels
{
    public sealed class CoordinateSystemAxesViewModel : ViewModelBase
    {
        #region Fields
        private CoordinateSystemElements coordinateSystemElements;
        private Tuple<Point, Point> xAxis;
        private Tuple<Point, Point> yAxis;
        private SolidColorBrush stroke;
        private double strokeThickness;
        #endregion

        #region Properties

        public Tuple<Point, Point> XAxis
        {
            get { return this.xAxis; }
            set { SetProperty(ref this.xAxis, value); }
        }

        public Tuple<Point, Point> YAxis
        {
            get { return this.yAxis; }
            set { SetProperty(ref this.yAxis, value); }
        }

        public SolidColorBrush Stroke
        {
            get { return this.stroke; }
            set { SetProperty(ref this.stroke, value); }
        }

        public double StrokeThickness
        {
            get { return this.strokeThickness; }
            set { SetProperty(ref this.strokeThickness, value); }
        }

        #endregion

        #region Constructors
        public CoordinateSystemAxesViewModel()
        {
            this.InitializeProperties();
        }
        #endregion

        #region Methods
        public override void InitializeProperties()
        {
            this.coordinateSystemElements = new CoordinateSystemElements();
            this.XAxis = this.coordinateSystemElements.XAxis;
            this.YAxis = this.coordinateSystemElements.YAxis;
            this.Stroke = this.coordinateSystemElements.Stroke;
            this.StrokeThickness = this.coordinateSystemElements.StrokeThickness;
        }

        public override void InitializeEvents()
        {
        }

        public override void Dispose()
        {
        }
        #endregion
    }
}
