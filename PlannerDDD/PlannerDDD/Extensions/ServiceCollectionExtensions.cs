using System;
using Domain.Entities.WorkItems;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlannerDDD.Services.WorkItems;

namespace PlannerDDD.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
                .AddScoped<IWorkItemRepository, WorkItemRepository>();
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
                .AddScoped<WorkItemService>();
        }
    }
}
