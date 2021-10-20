using AutoMapper;
using Domain.Entities;
using PlannerDDD.ViewModels;

namespace PlannerDDD.MapperConfig
{
    public class UserMappingConfig : Profile
    {
        public UserMappingConfig()
        {
            CreateMap<UserProfile, UserViewModel>();
        }
    }
}
