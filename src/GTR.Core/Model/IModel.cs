#region

using GTR.Core.Util;

#endregion

namespace GTR.Core.Serialization
{
    public interface IModel
    {
    }

    public class ObservableModel : ObservableObject, IModel
    {
    }
}