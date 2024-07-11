using CatalogAPI.Context;
using CatalogAPI.Extensions;
using CatalogAPI.Filters;
using CatalogAPI.Models;
using CatalogAPI.Repositories;
using CatalogAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(typeof(ApiExceptionFilter)));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                                 .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddSingleton<IBasicService, BasicService>();
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

// get apiSettings values
var apiSettings = builder.Configuration.Get<AppSettings>();
var projectName = apiSettings?.ProjectName ;
var defaultTimeout = apiSettings?.DefaultTimeOut;

// builder.Services.Configure<ApiBehaviorOptions>(options => options.DisableImplicitFromServicesParameters = true);

var app = builder.Build();

/*
 * examples of Middleware
app.Use(async (context, next) => 
{
    // Code before Middleware ...
    await next(context);
    // Code after Middleware ...
});
*/

/*
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Last Middleware executed.");
});
*/

// Makes the app use the error middle
app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
