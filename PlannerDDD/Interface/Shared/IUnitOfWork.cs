using System.Threading.Tasks;
using Domain.Base;

namespace Interface.Shared
{
    /// <summary>
    /// Function definitions for methods that are used for saving changes in database and creating async repo for specified entity
    /// </summary>
    public interface IUnitOfWork
    {
        // The fucntion to save any changes to the database
        Task<int> SaveChangesAsync();

        // The function to return async repository of the entity to work on
        IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity;
    }
}