using System;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RestAPI.Components.CQRS;

namespace RestAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            services.AddSingleton<ICommandBus, CommandBus>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // app.UseExceptionHandler("/Home/Error");
            }

            // app.UseStaticFiles();

            // app.UseIdentity();

            app.UseMvc();

            ICommandBus commandBus = app.ApplicationServices.GetService<ICommandBus>();
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            ConfigurationService.RegisterHandlers(commandBus, entryAssembly);
        }
    }
}
