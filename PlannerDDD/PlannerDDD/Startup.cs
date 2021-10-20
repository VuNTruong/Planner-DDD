using System.Text.Json.Serialization;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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

            // Add Identity with default configurations for User into Identity Role
            // Use EF to save information about Identity
            // Add Token provider
            // We MUST ADD user manager and sign in manager here
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<EFContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>();

            // Add authentication and authorization
            services.AddAuthentication();
            services.AddAuthorization();

            services.Configure<IdentityOptions>(options =>
            {
                // Configure password
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                // Configure email unique
                options.User.RequireUniqueEmail = true;
            });

            // Add auto mapper
            services.AddAutoMapper(typeof(Startup));

            // Configure JSON result
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

            // Add authentication and authorization
            // authentication MUST BE placed before authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
