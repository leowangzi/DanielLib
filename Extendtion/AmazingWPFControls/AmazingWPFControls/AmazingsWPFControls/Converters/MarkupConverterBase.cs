using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows.Data;

namespace AmazingWPFControls.Converters
{
    /// <summary>
    /// A base class for creation markup converter in your XAML.
    /// </summary>
    /// <typeparam name="T"> The final converter</typeparam>
    public abstract class MarkupConverterBase<T> : MarkupExtension, IValueConverter where T : class,new()
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkupConverterBase&lt;T&gt;"/> class.
        /// </summary>
        public MarkupConverterBase()
            : base()
        {
        }

        //Static instance of the current converter
        private static T _converter = new T();

        /// <summary>
        /// Returns the converter which is nearly a singleton
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter;
        }

        /// <summary>
        /// Method to override with the implementation of your converter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);

        /// <summary>
        /// Default convert back method which returns the same value but can be overriden
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
