using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.WorkItems;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class WorkItemRepository : RepositoryBase<WorkItem>, IWorkItemRepository
    {
        // The database context
        private EFContext databaseContext;

        // Constructor which will initialize database context from the base class
        public WorkItemRepository(EFContext dbContext) : base(dbContext)
        {
            // Initialize database context
            databaseContext = dbContext;
        }

        // The function to get all work items in the database
        public async Task<List<WorkItem>> ListWorkItemsAsync()
        {
            var workItems = await databaseContext.WorkItems.Include(w => w.Creator).ToListAsync();
            return workItems;
        }
    }
}
