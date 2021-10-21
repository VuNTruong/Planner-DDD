using System;
using AutoMapper;
using Domain.Entities;
using PlannerDDD.ViewModels;

namespace PlannerDDD.MapperConfig
{
    public class RoleMappingConfig : Profile
    {
        public RoleMappingConfig()
        {
            CreateMap<RoleDetailUserProfile, RoleAssignmentViewModel>();
        }
    }
}
