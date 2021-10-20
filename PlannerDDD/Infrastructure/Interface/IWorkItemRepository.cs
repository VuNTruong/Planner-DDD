using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface.Shared;

namespace Infrastructure.Interface
{
    /// <summary>
    /// Function definitions for extension of WorkItem
    /// </summary>
    public interface IWorkItemRepository : IAsyncRepository<WorkItem>
    {
        // The function to get list of all work items
        public Task<List<WorkItem>> ListWorkItemsAsync();

        // The function to get list of work items of user with specified user id
        public Task<List<WorkItem>> ListWorkitemsOfUserAsync(int userId);
    }
}
