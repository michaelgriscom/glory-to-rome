#region

using System;
using System.Globalization;
using System.Windows.Data;
using GTR.Core.Game;

#endregion

namespace GTR.Windows.Converters
{
    [ValueConversion(typeof (RoleType), typeof (string))]
    public class RoleTypeToMaterialTypeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoleType role = (RoleType) value;
            MaterialType material = role.ToMaterial();
            return material.ToString().ToUpperInvariant();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}