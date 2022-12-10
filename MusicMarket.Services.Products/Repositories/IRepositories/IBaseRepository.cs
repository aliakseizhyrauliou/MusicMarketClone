using MusicMarket.Services.Products.DbStuff.DbModels;

namespace MusicMarket.Services.Products.Repositories.IRepositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<T> SaveAsync(T entity);   
        Task SaveListAsync(IEnumerable<T> entities);
        void DeleteByIdAsync(long id);
        T Save(T entity);
    }
}
