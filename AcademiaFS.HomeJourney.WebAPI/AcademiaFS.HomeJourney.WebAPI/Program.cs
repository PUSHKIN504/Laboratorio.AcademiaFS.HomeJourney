//using AcademiaFS.HomeJourney.WebAPI._Features;
//using AcademiaFS.HomeJourney.WebAPI._Features.Generals;
//using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
//using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
////using Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName;
////using Farsiman.Extensions.Configuration;
//using Microsoft.EntityFrameworkCore;
//using Scrutor;
//using System.Text.Json.Serialization;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
//    });
//builder.Services.AddControllers();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        corsBuilder =>
//        {
//            if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Staging"))
//            {
//                corsBuilder
//                .SetIsOriginAllowed(_ => true)
//                .AllowAnyHeader()
//                .AllowAnyMethod()
//                .AllowCredentials();
//            }
//            else
//            {
//                corsBuilder
//                .WithOrigins("https://*.grupofarsiman.com", "https://*.grupofarsiman.io")
//                .SetIsOriginAllowedToAllowWildcardSubdomains()
//                .AllowAnyHeader()
//                .AllowAnyMethod()
//                .AllowCredentials();
//            }
//        });
//});

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//        options.JsonSerializerOptions.WriteIndented = true;
//    });
//builder.Services.AddDbContext<HomeJourneyContext>(o => o.UseSqlServer(
//                //builder.Configuration.GetConnectionStringFromENV("LOGISTIC_GFS")
//                builder.Configuration.GetConnectionString("LOGISTIC_GFS")
//            ));


//DependencyInjection.AppAplication(builder.Services);







//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors("AllowSpecificOrigin");

//app.UseAuthorization();

//app.UseAuthentication();

////app.UseFsAuthService();

//app.MapControllers();

//app.Run();
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", corsBuilder =>
            {
                if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Staging"))
                {
                    corsBuilder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
                else
                {
                    corsBuilder
                        .WithOrigins("https://*.grupofarsiman.com", "https://*.grupofarsiman.io")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddDbContext<HomeJourneyContext>(o => o.UseSqlServer(
        //    builder.Configuration.GetConnectionString("LOGISTIC_GFS")
        //));

        var isTesting = builder.Environment.IsEnvironment("Test");

        builder.Services.AddDbContext<HomeJourneyContext>(options =>
        {
            if (isTesting)
            {
                options.UseInMemoryDatabase("TestDatabase"); 
            }
            else
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("LOGISTIC_GFS"));
            }
        });
        DependencyInjection.AppAplication(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowSpecificOrigin");

        app.UseAuthorization();

        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }}