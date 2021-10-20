using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannerDDD.ViewModels;

namespace PlannerDDD.Controllers
{
    [Route("api/v1/workitems")]
    [ApiController]
    [Authorize]
    public class WorkItemController
    {
        // Work item service
        private readonly IWorkItemService _workItemService;

        // Auto mapper
        private IMapper _mapper;

        // Constructor
        public WorkItemController(IWorkItemService workItemService, IMapper mapper)
        {
            // Initialize work item service
            _workItemService = workItemService;

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to get all work items
        [Authorize(Roles = "Admin")]
        [HttpGet("getAllWorkItems")]
        public async Task<JsonResult> Get()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get response
            var workItems = await _workItemService.GetAllWorkItems();

            // Map list of work items into list of work item view models
            var workItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", workItemViewModels);
            
            // Return the response
            return new JsonResult(responseData);
        }

        // The function to get work items of the currently logged in user
        [HttpGet("getWorkItemsOfCurrentUser")]
        public async Task<JsonResult> GetWorkItemsOfCurrentUser()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get list of work items of current user
            var workItems = await _workItemService.GetAllWorkItemsOfCurrentUser();

            // Map list of work items into list of work item view models
            var workItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", workItemViewModels);

            // Return the response
            return new JsonResult(responseData);
        }

        // The function to create the new work item
        [HttpPost]
        public async Task<JsonResult> Create([FromBody] WorkItemViewModel workItemViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to create new work item
            var newWorkItem = await _workItemService.CreateNewWorkItem(workItemViewModel.Title, workItemViewModel.Content, workItemViewModel.DateCreated, workItemViewModel.CreatorId);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", newWorkItem);

            // Return the response
            return new JsonResult(responseData);
        }

        // The function to create new work item created by current user
        [HttpPost("createNewWorkItemForCurrentUser")]
        public async Task<JsonResult> CreateNewWorkItemByCurrentUser([FromBody] WorkItemViewModel workItemViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to create new work item
            var newWorkItem = await _workItemService.CreateNewWorkItemByCurrentUser(workItemViewModel.Title, workItemViewModel.Content);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", newWorkItem);

            // Return the response
            return new JsonResult(responseData);
        }

        // The function to update a work tiem
        [HttpPatch]
        public async Task<JsonResult> Update([FromQuery] int workItemId, [FromBody] WorkItemViewModel workItemViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to update a work item
            var updatedWorkItem = await _workItemService.UpdateWorkItem(workItemId, workItemViewModel.Title, workItemViewModel.Content);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", updatedWorkItem);

            // Return the response
            return new JsonResult(responseData);
        }

        // The function to delete a work item
        [HttpDelete]
        public async Task<JsonResult> Delete([FromQuery] int workItemId)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to remove a work item
            var removeWorkItemResult = await _workItemService.DeleteWorkItem(workItemId);

            // Add data to the response data
            if (removeWorkItemResult)
            {
                responseData.Add("status", "Done");
                responseData.Add("data", "Work item has been removed");
            } else
            {
                responseData.Add("status", "Not done");
                responseData.Add("data", "Work item was not removed");
            }

            // Return the response
            return new JsonResult(responseData);
        }
    }
}
