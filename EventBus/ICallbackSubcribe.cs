namespace Core.EventBus
{
    public interface ICallbackSubcribe<TMessage>
    {
        void Callback(TMessage message);
    }
}