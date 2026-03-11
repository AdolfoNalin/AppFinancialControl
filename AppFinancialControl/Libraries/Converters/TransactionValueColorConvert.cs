using AppFinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AppFinancialControl.Libraries.Converters
{
    public class TransactionValueColorConvert : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                Transaction transaction = (Transaction)value
                    ?? throw new ArgumentNullException();

                if (transaction.Type == TransactionType.Income)
                {
                    return Colors.Green;
                }
                else
                {
                    return Colors.Red;
                }
            }
            catch(ArgumentNullException ane)
            {
                return null;
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
