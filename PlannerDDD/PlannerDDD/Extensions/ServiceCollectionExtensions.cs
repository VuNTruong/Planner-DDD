using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Interface;
using Infrastructure.Interface.Shared;
using Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace PlannerDDD.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
                .AddScoped<IWorkItemRepository, WorkItemRepository>()
                .AddScoped<IUserProfileRepository, UserProfileRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IRoleDetailUserProfileRepository, RoleDetailUserProfileRepository>();
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services
            , IConfiguration configuration)
        {
            return services.AddDbContext<EFContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IWorkItemService, WorkItemService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<ISendEmailService, SendEmailService>();
        }
    }
}
