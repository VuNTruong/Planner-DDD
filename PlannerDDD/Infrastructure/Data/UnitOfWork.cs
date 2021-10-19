using System.Threading.Tasks;
using Domain.Base;
using Domain.Interfaces;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _dbContext;

        public UnitOfWork(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        // The function which will return repository base of the entity which need to be worked on
        public IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity
        {
            // Return the repos base (this will contain CRUD functions needed for queries)
            return new RepositoryBase<T>(_dbContext);
        }

        // The function to save changes to database
        public Task<int> SaveChangesAsync()
        {
            // Save changes
            return _dbContext.SaveChangesAsync();
        }
    }
}
