using AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Infrastructure.Repositories.EntityFrameworkCore
{
    public class BaseEntityFrameworkCoreRepository<T> : IEfRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseEntityFrameworkCoreRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            UpdateNewEntityProperties(entity);

            await _dbSet.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task AddMany(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                UpdateNewEntityProperties(entity);
            }

            await _dbSet.AddRangeAsync(entities);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckHealth()
        {
            return await _dbContext.Database.CanConnectAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var key = new object[] { id };

            var entity = await _dbSet.FindAsync(key);

            return entity;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            entity.LastUdpatedAt = DateTime.UtcNow;

            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        private void UpdateNewEntityProperties(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.LastUdpatedAt = DateTime.UtcNow;
        }
    }
}
