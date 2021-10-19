using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        // The function to add new entity
        Task<T> AddAsync(T entity);

        // The function to update an entity
        Task<T> UpdateAsync(T entity);

        // The function to delete an entity
        Task<bool> DeleteAsync(T entity);

        // The function to get the first entity that matches the specified expression
        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        // The function to list all entities that match the specified expression
        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
    }
}