namespace Core.Data.Contract.EventBus
{
    public interface IMessageType
    {
        string IdMessage { get; set; }

        string Status { get; set; }
    }
}