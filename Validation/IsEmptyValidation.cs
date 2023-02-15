using System.Globalization;
using System.Windows.Controls;

namespace SupplyApp.Validation
{
    internal class IsEmptyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(false, "Обязательное для заполнения");

            return new ValidationResult(true, "");
        }
    }
}
