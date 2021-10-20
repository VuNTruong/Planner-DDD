using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Used to implement extensions for WorkItem apart from basic CRUD functions
    /// </summary>
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
