#region

using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using GTR.Core.Game;

#endregion

namespace GTR.Universal.Converters
{
    public class RoleTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
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
            switch (role)
            {
                    case RoleType.Architect:
                    return new SolidColorBrush(Colors.Gray);
                    case RoleType.Craftsman:
                    return new SolidColorBrush(Colors.Green);
                    case RoleType.Laborer:
                    return new SolidColorBrush(Colors.Yellow);
                    case RoleType.Legionnaire:
                    return new SolidColorBrush(Colors.Red);
                    case RoleType.Merchant:
                    return new SolidColorBrush(Colors.Blue);
                    case RoleType.Patron:
                    return new SolidColorBrush(Colors.Purple);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}