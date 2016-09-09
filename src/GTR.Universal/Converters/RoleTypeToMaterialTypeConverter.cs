#region

using System;
using Windows.UI.Xaml.Data;
using GTR.Core.Game;

#endregion

namespace GTR.Universal.Converters
{
    public class RoleTypeToMaterialTypeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            RoleType role = (RoleType) value;
            MaterialType material = role.ToMaterial();
            return material.ToString().ToUpperInvariant();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}