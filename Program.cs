
using Api_1.Model;
using Api_1.Repository;

namespace Api_1;

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
        builder.Services.AddScoped<EducationalPlatformContext>();
        builder.Services.AddScoped<StudentRepository>();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        var looger = app.Logger;
        app.Use(async (context, next) =>
        {
            looger.LogInformation("prosaa request");
            await next(context);
            looger.LogInformation("prosses reaspons");
        });


        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
