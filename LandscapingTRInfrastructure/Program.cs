using LandscapingTRInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LandscapingTRDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("LandscapingTRConnectionString")));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
