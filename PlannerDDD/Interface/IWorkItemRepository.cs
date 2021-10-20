using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Interface.Shared;

namespace Interface
{
    /// <summary>
    /// Function definitions for extension of WorkItem
    /// </summary>
    public interface IWorkItemRepository : IAsyncRepository<WorkItem>
    {
        // The function to get list of all work items
        public Task<List<WorkItem>> ListWorkItemsAsync();
    }
}
