using Like.Domain;
using Like.RabbitMQ;
using Like.Services.Interfaces;
using Like.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Like.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddControllers();

            services.AddCors();

            services.AddTransient<IBlogService, BlogService>();

            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<LikeContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("LikeDB")));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Like",
                    Version = "v1",
                    Description = "",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Raidy Machado",
                        Url = new System.Uri("https://github.com/raidymachadohub")
                    }
                });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseCors("ApiCorsPolicy");

            app.UseSwagger();

            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json",
                "API Like"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });




        }
    }
}
