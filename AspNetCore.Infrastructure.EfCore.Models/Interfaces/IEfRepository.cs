using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Infrastructure.EfCore.Models.Interfaces
{
    public interface IEfRepository<T> where T : BaseEntity
    {
        Task<T> Add(T model);

        Task AddMany(IEnumerable<T> models);

        Task<bool> CheckHealth();

        Task Delete(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task Update(T model);

        Task Save();
    }
}
