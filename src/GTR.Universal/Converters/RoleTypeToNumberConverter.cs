#region

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
using GTR.Core.Game;

#endregion

namespace GTR.Universal.Converters
{
    public class RoleTypeToWorthConverter : IValueConverter
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
            return role.ToMaterial().MaterialWorth();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}