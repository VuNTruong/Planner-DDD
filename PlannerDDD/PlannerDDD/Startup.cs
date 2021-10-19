using System.Text.Json.Serialization;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlannerDDD.Extensions;

namespace PlannerDDD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Application services
            services
                .AddDatabase(Configuration)
                .AddUnitOfWork()
                .AddRepositories()
                .AddBusinessServices();

            services.AddDbContext<EFContext>(options =>
            {
                // Get connection string
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                // Establish connection
                options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
                
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
