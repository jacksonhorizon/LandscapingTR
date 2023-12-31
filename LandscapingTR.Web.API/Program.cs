using System.Reflection;
using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Services;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
builder.Services.AddTransient<ITimeEntryService, TimeEntryService>();
builder.Services.AddTransient<ITimeEntryHistoryRepository, TimeEntryHistoryRepository>();
builder.Services.AddTransient<ITimeEntryHistoryService, TimeEntryHistoryService>();
builder.Services.AddTransient<IJobRepository, JobRepository>();
builder.Services.AddTransient<IJobService, JobService>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<ILocationRepository, LocationRepository>();
builder.Services.AddTransient<ILocationService, LocationService>();


// ConfigureServices
builder.Services.AddCors(options =>
{
    // Default Policy
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:44464", "http://localhost:5028")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


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

// Configure
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

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

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    // Get the database context
    var dbContext = serviceProvider.GetRequiredService<LandscapingTRDbContext>();

    // Ensure that the database is created
    ////dbContext.Database.
    //var alreadyCreated = await dbContext.Database.EnsureCreatedAsync();

    //if (!alreadyCreated)
    //{
    //    // Apply any pending migrations
    //    dbContext.Database.Migrate();
    //}

    try
    {
        // Attempt to open a connection to the database
        dbContext.Database.OpenConnection();
    }
    catch (Exception)
    {
        dbContext.Database.Migrate();
    }
    finally
    {
        dbContext.Database.CloseConnection();
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
