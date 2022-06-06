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
            if (input == string.Empty) return new ValidationResult(false, "Entry is required.");
            if (!double.TryParse(input, out proposedValue)) return new ValidationResult(false, "Response is invalid.");
            if (proposedValue > 170.00) return new ValidationResult(false, "Value cannot be greater than 170.");
            if (proposedValue < -170.00) return new ValidationResult(false, "Value must be greater or equal -170.");
            return new ValidationResult(true, null);
        }
    }
}
