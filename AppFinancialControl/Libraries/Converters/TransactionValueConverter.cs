using AppFinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AppFinancialControl.Libraries.Converters
{
    public class TransactionValueConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {

                Transaction transaction = (Transaction)value
                ?? throw new ArgumentNullException();

                if (transaction.Type == TransactionType.Income)
                {
                    return transaction.Value.ToString("C");
                }
                else
                {
                    return $"-{transaction.Value.ToString("C")}";
                }
            }
            catch(ArgumentNullException ane)
            {
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public object? ConvertBack(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
