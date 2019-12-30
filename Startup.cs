using System;
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
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using web.api.todo.DAL;

namespace web.api.todo {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Global.getInstance().Configration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {

            services.AddCors();

            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<TODOContext>(options => options.UseSqlServer(Global.getInstance().Configration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Todo API",
                    Description = "Todo API with ASP.NET Core 3.0",
                    Contact = new OpenApiContact {
                        Name = "Batuhan Demir",
                        Email = "batuhandemirp@gmail.com",
                        Url = new Uri("https://github.com/batdemir/web.api.todo_dotnet.core")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            services
                .AddAuthentication(jwt => {
                    jwt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    jwt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters() {
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Global.getInstance().Configration["Issuer"],
                        ValidAudience = Global.getInstance().Configration["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Global.getInstance().Configration["SigningKey"]))
                    };
                });

            services.AddSingleton<IViewUser, ViewUser>();

            services.AddSingleton(new MapperConfiguration(mc => { mc.AddProfile(new UserMapper()); }).CreateMapper());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1"); });
        }
    }
}
