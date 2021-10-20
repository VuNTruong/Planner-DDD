using AutoMapper;
using Domain.Entities;
using PlannerDDD.ViewModels;

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
