using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.WorkItems;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PlannerDDD.ViewModels.WorkItems;

namespace PlannerDDD.Services.WorkItems
{
    public class WorkItemService : BaseService
    {
        // Work item repository
        private IWorkItemRepository _workItemRepository;

        // Auto mapper
        private IMapper _mapper;

        // Constructor
        public WorkItemService(IUnitOfWork unitOfWork, IWorkItemRepository workItemRepository, IMapper mapper) : base(unitOfWork)
        {
            // Instantiate work item repository
            _workItemRepository = workItemRepository;

            // Instantiate mapper
            _mapper = mapper;
        }

        // The function to get all work items in the database
        public async Task<JsonResult> GetAllWorkItems()
        {
            // Prepare response data for the client
            var responeData = new Dictionary<string, object>();

            // Call the function from repo to get all work items in the database
            var workItems = await _workItemRepository.ListWorkItemsAsync();

            // Map from work item work item to work item view model
            var workItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            // Add data to the repsonse data
            responeData.Add("status", "Done");
            responeData.Add("data", workItemViewModels);

            // Return the response
            return new JsonResult(responeData);
        }

        // The function to create new work item in the database
        public async Task<JsonResult> CreateNewWorkItem(WorkItemViewModel workItemViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Create work item async repository
            var workItemAsyncRepo = UnitOfWork.AsyncRepository<WorkItem>();

            // Map from work item view model to work item model
            var newWorkItem = _mapper.Map<WorkItem>(workItemViewModel);

            // Call the function to create new work item and return the created one
            var createdWorkItem = await workItemAsyncRepo
                .AddAsync(newWorkItem);

            // Save changes
            await UnitOfWork.SaveChangesAsync();

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", createdWorkItem);

            // Return the response
            return new JsonResult(responseData);
        }

        // The function to update work item in the database
        public async Task<JsonResult> UpdateWorkItem(int workItemId, WorkItemViewModel workItemViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Create work item async repository
            var workItemAsyncRepo = UnitOfWork.AsyncRepository<WorkItem>();

            // Get the work item that needs to be updated
            var workItemToUpdate = await workItemAsyncRepo.GetAsync(WorkItem => WorkItem.Id == workItemId);

            // Update the work item
            workItemToUpdate.UpdateContent(workItemViewModel.Content);
            workItemToUpdate.UpdateTitle(workItemViewModel.Title);
            
            // Call the function to update the work item
            var updatedWorkItem = await workItemAsyncRepo.UpdateAsync(workItemToUpdate);

            // Save changes
            await UnitOfWork.SaveChangesAsync();

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", updatedWorkItem);

            // Return the response
            return new JsonResult(responseData);
        }

        // The function to delete a work item
        public async Task<JsonResult> DeleteWorkItem(int workItemId)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Create work item async repository
            var workItemAsyncRepo = UnitOfWork.AsyncRepository<WorkItem>();

            // Get the work item that needs to be removed
            var workItemToRemove = await workItemAsyncRepo.GetAsync(workItem => workItem.Id == workItemId);

            // Call the function to remove a work item
            var removeResult = await workItemAsyncRepo.DeleteAsync(workItemToRemove);

            // Add data to the response data
            if (removeResult)
            {
                responseData.Add("status", "Done");
                responseData.Add("data", "Work item has been removed");
            } else
            {
                responseData.Add("status", "Not done");
                responseData.Add("data", "Work item was not removed");
            }

            // Save changes
            await UnitOfWork.SaveChangesAsync();

            // Return the response
            return new JsonResult(responseData);
        }
    }
}
