#region

using System;
using System.Globalization;
using System.Windows.Data;
using GTR.Core.Game;

#endregion

namespace GTR.Windows.Converters
{
    [ValueConversion(typeof (RoleType), typeof (string))]
    [ValueConversion(typeof (MaterialType), typeof (string))]
    public class EnumTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ToUpperInvariant();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MaterialType)
            {
                return Enum.Parse(typeof (MaterialType), value.ToString(), true);
            }
            return Enum.Parse(typeof (RoleType), value.ToString(), true);
        }
    }
}