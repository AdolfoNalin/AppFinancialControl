using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AppFinancialControl.Libraries.Converters
{
    public class TransactionNameConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                string result = (String)value ?? 
                    throw new ArgumentNullException("O campo nome não foi preenchido");

                return result.ToUpper().First();

            }
            catch(ArgumentNullException ane)
            {
                return null; 
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
