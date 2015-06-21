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
        void Display(string message, MessageType messageType = MessageType.Information);
    }
}