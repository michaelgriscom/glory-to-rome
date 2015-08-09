#region

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
using GTR.Core.Game;

#endregion

namespace GTR.Windows.Converters
{
    public class EnumTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString().ToUpperInvariant();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is MaterialType)
            {
                return Enum.Parse(typeof (MaterialType), value.ToString(), true);
            }
            return Enum.Parse(typeof (RoleType), value.ToString(), true);
        }
    }
}