using PlannerDDD.ViewModels.WorkItems;
using AutoMapper;
using Domain.Entities;

namespace PlannerDDD.MapperConfig
{
    public class WorkItemMappingConfig : Profile
    {
        public WorkItemMappingConfig()
        {
            CreateMap<WorkItem, WorkItemViewModel>();
            CreateMap<WorkItemViewModel, WorkItem>();
        }
    }
}
