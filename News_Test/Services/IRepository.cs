using System.Collections.Generic;
using System.Threading.Tasks;

namespace News_Test.Services
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<int> Add(TEntity entity);
        Task<int> Update(TEntity entity);
        Task<int> Delete(int id);
        bool Exists(int id);
    }
}
