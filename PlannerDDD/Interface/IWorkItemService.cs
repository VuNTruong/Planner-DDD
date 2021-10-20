using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Interface
{
    public interface IWorkItemService
    {
        // The function to get all work items
        public Task<List<WorkItem>> GetAllWorkItems();

        // The function to get all work items of the currently logged in user
        public Task<List<WorkItem>> GetAllWorkItemsOfCurrentUser();

        // The function to create new work item
        public Task<WorkItem> CreateNewWorkItem(string title, string content, string dateCreated, int creatorUserId);

        // The function to create new work item by the current user
        public Task<WorkItem> CreateNewWorkItemByCurrentUser(string title, string content);

        // The function to update work item
        public Task<WorkItem> UpdateWorkItem(int workItemId, string title, string content);

        // The function to delete work item
        public Task<bool> DeleteWorkItem(int workItemId);
    }
}
