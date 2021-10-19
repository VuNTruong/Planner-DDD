using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Base;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        // Constructor
        public RepositoryBase(EFContext dbContext)
        {
            // Initialize DbSet
            _dbSet = dbContext.Set<T>();
        }

        // The function to add and return new entity
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        // The function to delete an entity
        public Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }

        // The function to get a single entity that matches the specified expression
        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }

        // The function to list multiple entity that matches the specified expression
        public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToListAsync();
        }

        // The function to update an entity
        public Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
