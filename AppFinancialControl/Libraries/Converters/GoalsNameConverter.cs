using AppFinancialControl.Models;
using System.Globalization;

namespace AppFinancialControl.Libraries.Converters
{
    class GoalsNameConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                string result = (String)value ??
                  throw new ArgumentNullException("O campo nome não foi preenchido");

                return result.ToUpper().First();
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
