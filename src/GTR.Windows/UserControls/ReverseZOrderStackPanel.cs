#region

using System.Windows;
using System.Windows.Controls;

#endregion

namespace GTR.Windows.UserControls
{
    /// <summary>
    ///     Panel that reverses the Z Order of the children elements.
    /// </summary>
    public class ReverseZOrderStackPanel : StackPanel
    {
        private int _index = 9999;

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var size = base.ArrangeOverride(arrangeSize);

            foreach (UIElement child in Children)
                child.SetValue(ZIndexProperty, _index--);

            return size;
        }
    }
}