using FileNest.API.Configuration;
using FileNest.API.Exceptions;
using FileNest.API.Providers;
using MongoDB.Driver;

namespace FileNest.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure database settings
            builder.Services.Configure<DatabaseSettings>(
                builder.Configuration.GetSection("DatabaseSettings"));

            // Database connection provider
            builder.Services.AddSingleton<DatabaseConnectionProvider>();

            // Exception handling
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            // MongoDB client
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var provider =
                    sp.GetRequiredService<DatabaseConnectionProvider>();

                var connectionString =
                    provider.GetConnectionString("MongoDB");

                return new MongoClient(connectionString);
            });


            var app = builder.Build();

            app.UseExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsProduction() ||
                app.Environment.IsStaging() ||
                app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}