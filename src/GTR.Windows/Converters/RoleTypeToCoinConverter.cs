#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Shapes;
using GTR.Core.Game;

#endregion

namespace GTR.Windows.Converters
{
    [ValueConversion(typeof (RoleType), typeof (List<Ellipse>))]
    [ValueConversion(typeof (MaterialType), typeof (List<Ellipse>))]
    public class RoleTypeToCoinConverter : IValueConverter
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

            int circles = role.ToMaterial().MaterialWorth();

            var list = new List<Ellipse>();
            for (int i = 0; i < circles; i++)
            {
                var circle = new Ellipse();
                circle.Height = circle.Width = 20;
                list.Add(circle);
            }

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}