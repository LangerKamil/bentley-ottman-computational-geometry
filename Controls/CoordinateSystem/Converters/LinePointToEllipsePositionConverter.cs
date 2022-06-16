using System;
using System.Globalization;
using System.Windows.Data;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem.Converters
{
    public sealed class LinePointToEllipsePositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - 5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
