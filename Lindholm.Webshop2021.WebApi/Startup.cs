using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lindholm.Webshop2021.Core.IServices;
using Lindholm.Webshop2021.Domain.IRepositories;
using Lindholm.Webshop2021.Domain.Services;
using Lindholm.Webshop2021.EntityFramework;
using Lindholm.Webshop2021.EntityFramework.Entities;
using Lindholm.Webshop2021.EntityFramework.Repositories;
using Lindholm.Webshop2021.Security;
using Lindholm.Webshop2021.Security.Model;
using Lindholm.Webshop2021.Security.Services;
using Lindholm.Webshop2021.WebApi.Middleware;
using Lindholm.Webshop2021.WebApi.PolicyHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Lindholm.Webshop2021.WebApi
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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Lindholm.Webshop2021.WebApi", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            services.AddDbContext<MainDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=main.db"); 
            });
            services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=auth.db"); 
            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) 
                    //Configuration["JwtToken:SecretKey"]
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(CanWriteProductsHandler), 
                    policy => policy.Requirements.Add(new CanWriteProductsHandler()));
                options.AddPolicy(nameof(CanReadProductsHandler), 
                    policy => policy.Requirements.Add(new CanReadProductsHandler()));
            });
            services.AddCors(opt => opt
                .AddPolicy("dev-policy", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MainDbContext mainContext, AuthDbContext authDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lindholm.Webshop2021.WebApi v1"));
                app.UseCors("dev-policy");

                #region Setup Contexts
                
                mainContext.Database.EnsureDeleted();
                mainContext.Database.EnsureCreated();
                mainContext.Users.Add(new UserEntity { Name = "Bilbo" });
                mainContext.Users.Add(new UserEntity { Name = "Billy Bob" });
                mainContext.Users.Add(new UserEntity { Name = "Bobby Bill" });
                mainContext.SaveChanges();
                mainContext.Products.AddRange(
                    new ProductEntity{ Name = "P1", OwnerId = 1},
                    new ProductEntity{ Name = "P2", OwnerId = 1},
                    new ProductEntity{ Name = "P3", OwnerId = 1});
                mainContext.SaveChanges();
                
                authDbContext.Database.EnsureDeleted();
                authDbContext.Database.EnsureCreated();
                authDbContext.LoginUsers.Add(new LoginUser
                {
                    UserName = "nam",
                    HashedPassword = "ost",
                    DbUserId = 1,
                });
                authDbContext.LoginUsers.Add(new LoginUser
                {
                    UserName = "namnam",
                    HashedPassword = "ostost",
                    DbUserId = 2,
                });
                authDbContext.Permissions.AddRange(new Permission()
                {
                    Name = "CanWriteProducts"
                }, new Permission()
                {
                    Name = "CanReadProducts"
                });
                authDbContext.SaveChanges();
                authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 1, UserId = 1 });
                authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 2, UserId = 1 });
                authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 2, UserId = 2 });
                authDbContext.SaveChanges();
                

                #endregion
               
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseMiddleware<JWTMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}