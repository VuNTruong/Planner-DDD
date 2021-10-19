using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Entities.WorkItems
{
    public interface IWorkItemRepository : IAsyncRepository<WorkItem>
    {
        // The function to get list of all work items
        public Task<List<WorkItem>> ListWorkItemsAsync();
    }
}
