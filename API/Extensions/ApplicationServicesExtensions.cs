using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationservicesExtensions
    {
        public static IServiceCollection AddApplicationServices( this IServiceCollection services, 
        IConfiguration config)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(); 
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); 
            });
            services.AddSingleton<IConfigurationMultiplexer>(c=> 
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof (IGenericRepository<>),typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
            {
                var errors = ActionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0 ) // checking the error count is greater than 0
                    .SelectMany(x => x.Value.Errors) // flattening the list of errors
                    .Select(x =>x.ErrorMessage) // selecting the error messages
                    .ToArray(); // converting the collection to an array

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult (errorResponse);
            };
    });

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
            });
        });

            return services;
        }
    }
}