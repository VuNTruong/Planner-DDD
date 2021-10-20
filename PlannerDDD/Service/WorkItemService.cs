using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Interface;
using Interface.Shared;

namespace Services
{
    public class WorkItemService : BaseService
    {
        // Work item repository
        private IWorkItemRepository _workItemRepository;

        // Constructor
        public WorkItemService(IUnitOfWork unitOfWork, IWorkItemRepository workItemRepository) : base(unitOfWork)
        {
            // Instantiate work item repository
            _workItemRepository = workItemRepository;
        }

        // The function to get all work items in the database
        public async Task<List<WorkItem>> GetAllWorkItems()
        {
            // Call the function from repo to get all work items in the database
            var workItems = await _workItemRepository.ListWorkItemsAsync();

            // Return the data
            return workItems;
        }

        // The function to create new work item in the database
        public async Task<WorkItem> CreateNewWorkItem(string title, string content, string dateCreated, int creatorUserId)
        {
            // Create work item async repository
            var workItemAsyncRepo = UnitOfWork.AsyncRepository<WorkItem>();

            // Map from work item view model to work item model
            var newWorkItem = new WorkItem(title, content, dateCreated, creatorUserId);

            // Call the function to create new work item and return the created one
            var createdWorkItem = await workItemAsyncRepo
                .AddAsync(newWorkItem);

            // Save changes
            await UnitOfWork.SaveChangesAsync();

            // Return newly created work item object
            return createdWorkItem;
        }

        // The function to update work item in the database
        public async Task<WorkItem> UpdateWorkItem(int workItemId, string title, string content)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Create work item async repository
            var workItemAsyncRepo = UnitOfWork.AsyncRepository<WorkItem>();

            // Get the work item that needs to be updated
            var workItemToUpdate = await workItemAsyncRepo.GetAsync(WorkItem => WorkItem.Id == workItemId);

            // Update the work item
            workItemToUpdate.UpdateTitleAndWorkItem(title, content);
            
            // Call the function to update the work item
            var updatedWorkItem = await workItemAsyncRepo.UpdateAsync(workItemToUpdate);

            // Return the updated work item
            return updatedWorkItem;
        }

        // The function to delete a work item
        public async Task<bool> DeleteWorkItem(int workItemId)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Create work item async repository
            var workItemAsyncRepo = UnitOfWork.AsyncRepository<WorkItem>();

            // Get the work item that needs to be removed
            var workItemToRemove = await workItemAsyncRepo.GetAsync(workItem => workItem.Id == workItemId);

            // Call the function to remove a work item
            var removeResult = await workItemAsyncRepo.DeleteAsync(workItemToRemove);

            // Return the result
            return removeResult;
        }
    }
}
