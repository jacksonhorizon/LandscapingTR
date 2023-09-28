using System.Reflection;
using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Services;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Automapper config
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
// Add services to the container.
builder.Services.AddDbContext<LandscapingTRDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LandscapingTRConnectionString")));
builder.Services.AddTransient<IMapper>(_ => mapper);
builder.Services.AddTransient<ILookupRepository, LookupRepository>();
builder.Services.AddTransient<ILookupService, LookupService>();
builder.Services.AddTransient<ILookupService, LookupService>();
builder.Services.AddTransient<ITimeEntryRepository, TimeEntryRepository>();






// Add other Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "LandScapingTR.Web.API",
            Description = "The Web API for LandscapingTR",
            Version = "v1"
        });

    var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var filepath = Path.Combine(AppContext.BaseDirectory, filename);
});

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        options.DocumentTitle = "My Swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
