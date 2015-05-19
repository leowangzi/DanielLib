using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace DanielLib.Utilities.ConverterCollection
{
    [ValueConversion(typeof(double), typeof(int))]
    public class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double data_double = (double)value;
            return (int)data_double;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int data_int = (int)value;
            return (double)data_int;
        }
    }
}
