using System.Globalization;
using System.Windows.Controls;

namespace SupplyApp.Validation
{
    internal class NumericValidation : ValidationRule
    {
        public int Max { get; set; }
        public int Min { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!int.TryParse(value.ToString(), out int result))
                return new ValidationResult(false, $"Введите значение в пределах от {Min} до {Max}.");
            if (result >= Min && result <= Max)
                return new ValidationResult(true, "");
            else
                return new ValidationResult(false, $"Введите значение в пределах от {Min} до {Max}.");
        }
    }
}
