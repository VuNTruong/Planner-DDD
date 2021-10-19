using System;
using Domain.Entities.WorkItems;
using PlannerDDD.ViewModels.WorkItems;
using AutoMapper;

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
