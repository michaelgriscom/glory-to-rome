#region

using System;

#endregion

namespace GTR.Windows.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}