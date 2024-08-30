using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeed;
using Talabat.Repository.Identity;
using Talabat.Repository.Repositories;
using TalabatApi.Extensions;
using TalabatApi.Helper;

namespace TalabatApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            builder.Services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
            builder.Services.AddDbContext<StoreContext>(
                options => options
                            .UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

            builder.Services.AddSingleton<IConnectionMultiplexer>(options => {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);

            });

            builder.Services.AddIdentityService(builder.Configuration);
     

            var app = builder.Build();

            using var Scope =app.Services.CreateScope(); 
            var services =Scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();  

            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();
                await dbContext.Database.MigrateAsync();
                var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityDbContext.Database.MigrateAsync();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();

                await AppIdentityDbSeed.UserSeedAsync(userManager);
                await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex) {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"An Error Occured during applying database migration");
            
            }
    




                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
