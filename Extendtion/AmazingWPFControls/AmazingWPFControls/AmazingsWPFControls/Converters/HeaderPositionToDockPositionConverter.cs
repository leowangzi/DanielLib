using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;

namespace AmazingWPFControls.Converters
{
    [ValueConversion(typeof(AmazingWPFControls.HeaderedControl.HeaderPosition), typeof(Dock))]
    public class HeaderPositionToDockPositionConverter : MarkupConverterBase<HeaderPositionToDockPositionConverter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderPositionToDockPositionConverter"/> class.
        /// </summary>
        public HeaderPositionToDockPositionConverter() : base() { }

        /// <summary>
        /// Converts a value from HeaderPosition to a DockPosition
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Dock dockValue;
            if (value == null) return Dock.Top;

            bool conversionWorked = Enum.TryParse<Dock>(value.ToString(), out dockValue);

            if (conversionWorked) return dockValue;

            return Dock.Top;
        }

        /// <summary>
        /// Converts a value from DockPosition to a HeaderPosition
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            AmazingWPFControls.HeaderedControl.HeaderPosition dockValue;
            if (value == null) return Dock.Top;

            bool conversionWorked =
                Enum.TryParse<AmazingWPFControls.HeaderedControl.HeaderPosition>(value.ToString(), out dockValue);

            if (conversionWorked) return dockValue;

            return AmazingWPFControls.HeaderedControl.HeaderPosition.Top;
        }
    }
}
