using FileNest.API.Configuration;
using FileNest.API.Exceptions;
using FileNest.API.Services;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
            builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

            builder.Services.AddSingleton<DatabaseConnectionService>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddProblemDetails();
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var provider = sp.GetRequiredService<DatabaseConnectionService>();

                var connectionString = provider.GetConnectionString("MongoDB");

                return new MongoClient(connectionString);
            });

            builder.Services.AddSingleton<MongoDbService>();

            var app = builder.Build();

            app.UseExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsProduction() || app.Environment.IsStaging() || app.Environment.IsDevelopment())
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
