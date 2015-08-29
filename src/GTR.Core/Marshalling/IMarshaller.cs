namespace GTR.Core.Serialization
{
    public interface IMarshaller<T1, T2> where T1 : IModel where T2 : IDto
    {
        T2 Marshall(T1 fatRepresentation);

        T1 UnMarshall(T2 slimRepresentation);
    }
}