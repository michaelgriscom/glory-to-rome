#region

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using GTR.Core.Game;

#endregion

namespace GTR.Windows.Converters
{
    [ValueConversion(typeof (MaterialType), typeof (SolidColorBrush))]
    [ValueConversion(typeof (RoleType), typeof (SolidColorBrush))]
    public class RoleTypeToBrushConverter : IValueConverter
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
            return Application.Current.Resources[role + "Brush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}