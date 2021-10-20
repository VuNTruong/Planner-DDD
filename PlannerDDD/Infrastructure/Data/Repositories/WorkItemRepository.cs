using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface;
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
            // Reference the database to get list of work items in the database
            var workItems = await databaseContext.WorkItems.Include(w => w.Creator).ToListAsync();

            // Return list of work items
            return workItems;
        }

        // The function to get work items of user with specified user id
        public async Task<List<WorkItem>> ListWorkitemsOfUserAsync(int userId)
        {
            // Reference the database to get list of work items of user with specified user id
            var workItems = await databaseContext.WorkItems.Where(w => w.CreatorId == userId).ToListAsync();

            // Return list of work items
            return workItems;
        }
    }
}
