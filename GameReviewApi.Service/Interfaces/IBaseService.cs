namespace GameReviewApi.Service.Interfaces
{
    public interface IBaseService<T>
    {
        Task<T> GetByIdAsyncService(int id);
        Task<T> CreateAsyncService(T entity);
        Task<bool> DeleteAsyncService(int id);
        Task<T> UpdateAsyncService(T entity);
    }
}
