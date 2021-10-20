using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlannerDDD.ViewModels.WorkItems;
using Services;

namespace PlannerDDD.Controllers
{
    [Route("api/v1/workitems")]
    [ApiController]
    public class WorkItemController
    {
        // Work item service
        private readonly WorkItemService _workItemService;

        // Constructor
        public WorkItemController(WorkItemService workItemService)
        {
            // Initialize work item service
            _workItemService = workItemService;
        }

        // The function to get all work items
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get response
            var workItems = await _workItemService.GetAllWorkItems();

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", workItems);
            
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
