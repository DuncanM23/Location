using LocationCore.Interfaces;
using LocationCore.Services;
using LocationRepo;
using LocationRepo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocationAPI", Version = "v1" });
            });
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddDbContext<LocationDataContext>(options => options.UseSqlServer("Server=sql;Database=Location;Trusted_Connection=False;User Id=sa;Password=password1!;MultipleActiveResultSets=true"));
            //services.AddDbContext<LocationDataContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));  
            //services.AddDbContext<LocationDataContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

        }

        // This method gets called by the runtime. Use  the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LocationDataContext dataContext)
        {
         //TODO fix this in the docker-compose
            //if (env.IsDevelopment())this method to configure
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocationAPI v1"));
            //}

            dataContext.Database.Migrate();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
