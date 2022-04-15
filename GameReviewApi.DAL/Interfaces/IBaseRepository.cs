namespace GameReviewApi.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<bool> Delete(int id);
        Task<T> Update(T entity);
    }
}
