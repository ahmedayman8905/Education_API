
using Api_1.Entity;
using Api_1.Outherize;
using Api_1.Repository;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<token>();


        builder.Services.AddScoped<StudentRepository>();
        // Add Mapster
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        //builder.Services.AddIdentityApiEndpoints<userLogin>()
        //  .AddEntityFrameworkStores<EducationalPlatformContext>();



        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(o =>
        //{

        //    o.SaveToken = true;
        //    o.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J7MfAb4WcAIMkkigVtIepIILOVJEjacB")),
        //        ValidIssuer = "SurveyBasketApp",
        //        ValidAudience = "SurveyBasketApp users"

        //    };
        //});


        builder.Services.AddDependencies(builder.Configuration);

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

        app.UseCors();

        app.UseAuthorization();

        //app.MapIdentityApi<userLogin>();

        app.MapControllers();

        app.Run();
    }
}
