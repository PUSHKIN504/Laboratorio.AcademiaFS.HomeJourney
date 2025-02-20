//using Farsiman.Extensions.Configuration;
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
//using Farsiman.Extensions.Configuration;
using Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common;
//using Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName;
using Microsoft.EntityFrameworkCore;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsBuilder =>
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

builder.Services.AddDbContext<HomeJourneyContext>(o => o.UseSqlServer(
                //builder.Configuration.GetConnectionStringFromENV("LOGISTIC_GFS")
                builder.Configuration.GetConnectionString("LOGISTIC_GFS")
            ));


// Servicios de Aplicación
builder.Services.AddTransient<CommonService>();
builder.Services.AddTransient<PaisesService>();
builder.Services.AddTransient(typeof(IGenericServiceInterface<,>), typeof(GenericService<,>));


//builder.Services.Scan(scan => scan
//    .FromAssemblyOf<CommonService>()
//    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
//    .AsImplementedInterfaces()
//    .WithTransientLifetime());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.UseAuthentication();

//app.UseFsAuthService();

app.MapControllers();

app.Run();

