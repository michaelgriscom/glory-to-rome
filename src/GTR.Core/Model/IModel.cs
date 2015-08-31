#region

using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public interface IModel
    {
    }

    public class ObservableModel : ObservableObject, IModel
    {
    }
}