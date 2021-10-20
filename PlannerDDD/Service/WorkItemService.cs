using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Interface.Shared;
using Interface;

namespace Services
{
    public class WorkItemService : BaseService, IWorkItemService
    {
        // Work item repository
        private readonly IWorkItemRepository _workItemRepository;

        // User profile repository
        private readonly IUserProfileRepository _userprofileRepository;

        // Constructor
        public WorkItemService(IUnitOfWork unitOfWork, IWorkItemRepository workItemRepository, IUserProfileRepository userProfileRepository) : base(unitOfWork)
        {
            // Initialize work item repository
            _workItemRepository = workItemRepository;

            // Initialize user profile repository
            _userprofileRepository = userProfileRepository;
        }

        // The function to get all work items in the database
        public async Task<List<WorkItem>> GetAllWorkItems()
        {
            // Call the function from repo to get all work items in the database
            var workItems = await _workItemRepository.ListWorkItemsAsync();

            // Return the data
            return workItems;
        }

        // The function to get work items of the currently logged in user
        public async Task<List<WorkItem>> GetAllWorkItemsOfCurrentUser()
        {
            // Call the function to get user id of the currently logged in user
            var currentUserId = await _userprofileRepository.GetCurrentUserId();

            // Call the function to get all work items of the currenty logged in user
            var workItems = await _workItemRepository.ListWorkitemsOfUserAsync(currentUserId);

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

        // The function to create new work item by the currently logged in user
        public async Task<WorkItem> CreateNewWorkItemByCurrentUser(string title, string content)
        {
            // Create work item async repository
            var workItemAsyncRepo = UnitOfWork.AsyncRepository<WorkItem>();

            // Date time object which will be used for getting current date
            DateTime dateTime = DateTime.Now.Date;

            // Call the function to get user id of the currently logged in user
            var currentUserId = await _userprofileRepository.GetCurrentUserId();

            // Create the new work item
            var newWorkItem = new WorkItem(title, content, dateTime.ToString("MM/dd/yyyy"), currentUserId);

            // Call the function to create new work item and return the created one
            var createdWorkItem = await workItemAsyncRepo
                .AddAsync(newWorkItem);

            // Save changes
            await UnitOfWork.SaveChangesAsync();

            // Return the newly created work item object
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
            var workItemToUpdate = await workItemAsyncRepo.GetAsync(workItem => workItem.Id == workItemId);

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
