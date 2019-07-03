using AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Models;
using Repositories.EntityFrameworkCore.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.EntityFrameworkCore
{
    public class BaseInMemoryRepository<T> : IEfRepository<T> where T : DbIdEntity
    {
        private static ConcurrentDictionary<int, T> _entityIdToValue = new ConcurrentDictionary<int, T>();
        private static int _currentId = 1;

        public BaseInMemoryRepository()
        {

        }

        public async Task<T> Add(T model)
        {
            model.Id = _currentId;

            Interlocked.Increment(ref _currentId);

            model.CreatedAt = DateTime.UtcNow;
            model.LastUdpatedAt = DateTime.UtcNow;

            _entityIdToValue.TryAdd(model.Id, model);

            return model;
        }

        public async Task AddMany(IEnumerable<T> models)
        {
            var addTasks = models.Select(m => Add(m));

            await Task.WhenAll(addTasks);
        }

        public async Task<bool> CheckHealth()
        {
            return true;
        }

        public async Task Delete(int id)
        {
            _entityIdToValue.TryRemove(id, out var entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return _entityIdToValue.Values.ToList();
        }

        public async Task<T> GetById(int id)
        {
            _entityIdToValue.TryGetValue(id, out var entity);

            return entity;
        }

        public async Task Save()
        {
        }

        public async Task Update(T model)
        {
            var oldValue = await GetById(model.Id);

            _entityIdToValue.TryUpdate(model.Id, model, oldValue);
        }
    }
}
