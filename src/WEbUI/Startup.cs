using DocRouter.Application;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Infrastructure;
using DocRouter.Persistence;
using DocRouter.WebUI.Common;
using DocRouter.WebUI.Models;
using DocRouter.WebUI.Models.Validators;
using DocRouter.WebUI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace DocRouter.WebUI
{
    public class Startup
    {
        private IServiceCollection _services;
        /// <summary>
        /// The <see cref="IConfiguration"/>
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// The <see cref="IWebHostEnvironment"/>
        /// </summary>
        public IWebHostEnvironment Environment { get; }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="configuration">An implementation of <see cref="IConfiguration"/></param>
        /// <param name="environment">An implementation of <see cref="IWebHostEnvironment"/></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        /// <summary>
        /// Configures the Authentication protocol for the application
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <remarks>
        /// This method can be overridden in derived Startup classes to facilitate testing.
        /// </remarks>
        protected virtual void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme);
            services.AddScoped<IClaimsTransformation, ClaimsLoader>();
            
        }
        /// <summary>
        /// Configures dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <remarks>
        /// This method can be overridden in derived Startup classes to facilitate testing.
        /// </remarks>
        protected virtual void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        /// <summary>
        /// Configures the persistence layer.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <remarks>
        /// This method can be overridden in derived Startup classes to facilitate testing.
        /// </remarks>
        protected virtual void ConfigurePersistence(IServiceCollection services)
        {
            services.AddPersistence(Configuration);
        }
        /// <summary>
        /// Configures the infrastructure layer.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <remarks>
        /// This method can be overridden in derived Startup classes to facilitate testing.
        /// </remarks>
        protected virtual void ConfigureInfrastructure(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            services.AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IDocRouterContext>());
            services.AddApplication();
            services.AddHealthChecks()
                .AddDbContextCheck<DocRouterContext>();
            services.AddMvc();

            services.AddTransient<IValidator<SubmissionViewModel>, SubmissionViewModelValidator>();
            this.ConfigureAuth(services);
            this.ConfigureDependencies(services);
            this.ConfigurePersistence(services);
            this.ConfigureInfrastructure(services);
            _services = services;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error-local-development");
                RegisteredServicesPage(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseCustomExceptionHandler();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        /// <summary>
        /// Private method that creates an endpoint to view registered services in Development.
        /// </summary>
        /// <param name="app"></param>
        private void RegisteredServicesPage(IApplicationBuilder app)
        {
            app.Map("/services", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>Registered Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));
        }
    }
}
