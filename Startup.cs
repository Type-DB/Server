using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace TypeDB.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly Core Core;
        public readonly Instance Instance;

        public Startup(IConfiguration configuration)
        {
            this.Core = new Core(Mode.Standalone | Mode.OnlyForBinding);
            this.Instance = this.Core
                .Configure(new Configuration()
                {
                })
                .Build();

            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Core>(this.Core);
            services.AddSingleton<Instance>(this.Instance);

            services.AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Type-DB.Server",
                    Description = "💡 It's a serialized  in-memory and strongly typed database developed in C#",
                    Contact = new Contact
                    {
                        Name = "Type-DB Official Documentation",
                        Url = "https://type-db.gitbook.io/overview/"
                    },
                    License = new License
                    {
                        Name = "GNU General Public License v3.0",
                        Url = "https://github.com/Type-DB/Core/blob/master/LICENSE.md"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Default}/{action=Index}/{id?}");
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Type-DB.Server");
            });
        }
    }
}
