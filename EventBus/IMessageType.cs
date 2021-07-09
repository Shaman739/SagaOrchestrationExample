namespace ConsoleApp1.EventBus
{
    public interface IMessageType
    {
        string IdMessage { get; set; }

        string Status { get; set; }
    }
}