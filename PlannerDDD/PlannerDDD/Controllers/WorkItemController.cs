using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlannerDDD.Services.WorkItems;
using PlannerDDD.ViewModels.WorkItems;

namespace PlannerDDD.Controllers
{
    [ApiController]
    [Route("api/v1/workitems")]
    public class WorkItemController
    {
        private readonly WorkItemService _workItemService;

        public WorkItemController(WorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        // The function to get all work items
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            // Call the function to get response
            var workItems = await _workItemService.GetAllWorkItems();
            
            // Return the response
            return workItems;
        }

        // The function to create the new work item
        [HttpPost]
        public async Task<JsonResult> Create([FromBody] WorkItemViewModel workItemViewModel)
        {
            // Call the function to create new work item
            var newWorkItem = await _workItemService.CreateNewWorkItem(workItemViewModel);

            // Return the response
            return newWorkItem;
        }

        // The function to update a work tiem
        [HttpPatch]
        public async Task<JsonResult> Update([FromQuery] int workItemId, [FromBody] WorkItemViewModel workItemViewModel)
        {
            // Call the function to update a work item
            var updatedWorkItem = await _workItemService.UpdateWorkItem(workItemId, workItemViewModel);

            // Return the response
            return updatedWorkItem;
        }

        // The function to delete a work item
        [HttpDelete]
        public async Task<JsonResult> Delete([FromQuery] int workItemId)
        {
            // Call the function to remove a work item
            var removeWorkItem = await _workItemService.DeleteWorkItem(workItemId);

            // Return the response
            return removeWorkItem;
        }
    }
}
