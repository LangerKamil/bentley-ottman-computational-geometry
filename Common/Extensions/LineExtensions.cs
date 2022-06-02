using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace GeometriaObliczeniowa.Common.Extensions
{
    public static class LineExtensions
    {
        public static List<Line> GenerateEngineInput(this List<Line> lines, ObservableCollection<SegmentViewModel> segments)
        {
            segments.ForEach(x =>
            {
                lines.Add(new Line(new Point(x.StartingPointX, x.StartingPointY),
                    new Point(x.EndingPointX, x.EndingPointY)));

            });
            return lines;
        }
    }
}
