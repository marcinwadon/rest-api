using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RestAPI.Components.CQRS;
using RestAPI.Domain.Task.Command;
using RestAPI.Domain.Task.Command.Handler;

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

            this.ConfigureCommandBus(app);
        }

        private void ConfigureCommandBus(IApplicationBuilder app) {
            ICommandBus commandBus = app.ApplicationServices.GetService<ICommandBus>();
            commandBus.registerHandler<AddTask>(new AddTaskHandler());
        }
    }
}
