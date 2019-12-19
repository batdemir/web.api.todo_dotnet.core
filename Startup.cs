using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using web.api.todo.Models;
using web.api.todo.Models.DB;
using web.api.todo.Models.Mapper;

namespace web.api.todo {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Global.getInstance().Configration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<TODOContext>(options => options.UseSqlServer(Global.getInstance().Configration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Version = "v1",
                    Title = "Todo API",
                    Description = "Todo API with ASP.NET Core 3.0"
                });
            });
            services.AddSingleton<IViewUser, ViewUser>();
            services.AddSingleton(new MapperConfiguration(mc => {
                mc.AddProfile(new UserMapper());
            }).CreateMapper());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
            });
        }
    }
}
