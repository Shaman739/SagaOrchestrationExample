namespace Core.Data.Contract
{
    public interface IRepository<T>
    {
        bool GetAvailableItem(long id);
    }
}