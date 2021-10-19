using System.Threading.Tasks;
using Domain.Base;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        // The fucntion to save any changes to the database
        Task<int> SaveChangesAsync();

        // The function to return async repository of the entity to work on
        IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity;
    }
}