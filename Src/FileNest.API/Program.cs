using FileNest.API.Exceptions;
using FileNest.Service;
using FileNest.Service.Configuration;
using FileNest.Web.Mappings;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

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


            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODIwMzYxNjAwIiwiaWF0IjoiMTc4ODgzNjk4MyIsImFjY291bnRfaWQiOiIwMWEwN2VmZDg1Y2Y3MWZmYWIxZjg1MDNjYzBlY2U1MCIsImN1c3RvbWVyX2lkIjoiMDFhMDdlZmQ4NWNmNzFmZmFiMWY4NTAzY2MwZWNlNTAiLCJzdWJfaWQiOiItIiwiZWRpdGlvbiI6IjAiLCJ0eXBlIjoiMiJ9.C39qOqscKM_eEnAKCgc0GQYSc7qv-P26UzAGYAKuMlwfDjho0ZgnKP7q3Lwvn7jmjrB7fFF9wkpli_fk7eAgTWnBp3v1WxoidMdQmm_uZggTsaCNa5bc3PCFY_BF6KoUPc7CF7hvGMOZaJRxebmkuXohObR6qO9J0PSiTe7Gj6KsPJ4L0KXPCbGHXG2zV8WW721zS0paV5JkST7-PhfAhwelK3xRHoiYbqddNP6ZP5AOIRxovPWfAQNAhYd7UyewM3zkTgMgrjpneHyZU7p_GDZox0Bne2hWQp-YyFQ_XlnUs58um7gCsFOHfpyqmEa-t5cVhQHIHnswIZYjN5EcAA";
            }, typeof(Program));
            builder.Services.AddScoped<MongoDbService>();
            builder.Services.AddSingleton<UserService>();


            var app = builder.Build();

            app.UseExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsProduction() ||
                app.Environment.IsStaging())
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