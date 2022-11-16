using MusicMarket.Services.Auth.DbStuff.DbModels;

namespace MusicMarket.Services.Auth.DbStuff.Repositories.IRepositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(long id);
        Task<List<T>> GetAllAsync();
        Task SaveAsync(T dbModel);
        void Remove(T dbModel);
        Task RemoveAsync(long id);
        Task<bool> AnyAsync();
        bool Any();
        void SaveList(IEnumerable<T> dbModelsList);
    }
}
