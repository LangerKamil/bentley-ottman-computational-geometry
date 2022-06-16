using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Extensions;
using GeometriaObliczeniowa.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace GeometriaObliczeniowa.Engines.Models
{
    public sealed class IntersectionEngineInput
    {
        #region Properties
        public Line LineA { get; set; }

        public Line LineB { get; set; }

        public int Precision { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        ///  Generates an input for the IntersectionEngine.
        /// </summary>
        /// <param name="segments"> Observable collection of SegmentViewModel.</param>
        /// <param name="precision"> Precision number integer as an optional parameter. Default value is 5.</param>
        public IntersectionEngineInput(ObservableCollection<SegmentViewModel> segments, int precision = 5)
        {
            this.Precision = precision;
            this.GenerateEngineInput(segments);
        }
        #endregion

        #region Methods
        public double CalculateTolerance()
        {
            return Math.Round(Math.Pow(0.1, this.Precision), this.Precision);
        }

        private void GenerateEngineInput(ObservableCollection<SegmentViewModel> segments)
        {
            if (segments == null) { throw new ArgumentNullException(nameof(segments)); }

            List<Line> lines = new List<Line>();
            segments.ForEach(x =>
            {
                lines.Add(new Line(new Point(x.StartingPointX, x.StartingPointY),
                    new Point(x.EndingPointX, x.EndingPointY)));

            });
            this.LineA = lines[0];
            this.LineB = lines[1];
        }
        #endregion
    }
}
