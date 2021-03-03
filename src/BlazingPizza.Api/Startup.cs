using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazingPizza.Api
{
    public class Startup
    {
        private const string SPA_ORIGINS_POLICY = nameof(SPA_ORIGINS_POLICY);

        private readonly string[] ALLOWED_ORIGINS = new string[]
        {
            "",
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: SPA_ORIGINS_POLICY,
                                builder =>
                                {
                                    builder.WithOrigins(ALLOWED_ORIGINS)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(SPA_ORIGINS_POLICY);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
