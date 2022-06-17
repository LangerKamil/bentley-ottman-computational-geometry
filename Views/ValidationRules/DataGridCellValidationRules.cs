using System.Globalization;
using System.Windows.Controls;

namespace GeometriaObliczeniowa.Views.ValidationRules
{
    public sealed class DataGridCellValidationRules : ValidationRule

    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double proposedValue;
            string input = value.ToString();
            if (input == string.Empty) return new ValidationResult(false, "Pole nie może być puste.");
            if (!double.TryParse(input, out proposedValue)) return new ValidationResult(false, "Dozwolone tylko liczby całkowite.");
            if (proposedValue > 170.00) return new ValidationResult(false, "Wartość nie może być większa niż 170.");
            if (proposedValue < -170.00) return new ValidationResult(false, "Wartość nie może być mniejsza niż -170.");
            return new ValidationResult(true, null);
        }
    }
}
