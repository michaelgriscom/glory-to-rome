#region

using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Marshalling
{
    public interface IMarshaller<T1, T2> where T1 : IModel where T2 : IDto
    {
        T2 Marshall(T1 poco);
        T1 UnMarshall(T2 dto);
    }
}