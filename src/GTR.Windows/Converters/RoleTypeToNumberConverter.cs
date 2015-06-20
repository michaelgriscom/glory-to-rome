#region

using System;
using System.Globalization;
using System.Windows.Data;
using GTR.Core.Game;

#endregion

namespace GTR.Windows.Converters
{
    [ValueConversion(typeof (RoleType), typeof (int))]
    [ValueConversion(typeof (MaterialType), typeof (int))]
    public class RoleTypeToNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoleType role;
            if (value is MaterialType)
            {
                role = ((MaterialType) value).ToRole();
            }
            else
            {
                role = (RoleType) value;
            }
            return role.ToMaterial().MaterialWorth();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}