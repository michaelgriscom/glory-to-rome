namespace GTR.Core.Services
{
    public enum MessageType
    {
        Information,
        Warning,
        Error
    }

    public interface IMessageProvider
    {
        void Display(MessageType messageType, string message);
    }
}