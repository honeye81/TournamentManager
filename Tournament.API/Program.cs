using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Tournament.Data.Mapping;
using Tournament.Data.Repositories;
using Tournaments.Core.Contracts;
using Tournament.Services;
using System.Reflection;
using FluentValidation.AspNetCore;
using Tournaments.Core.Validators;
using Tournament.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Remove duplicate AddControllers call
builder.Services.AddControllers()
    .AddApplicationPart(typeof(Tournament.Presentation.Controllers.TournamentsController).Assembly)
    .AddNewtonsoftJson()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<TournamentForCreationDtoValidator>();
        fv.AutomaticValidationEnabled = true;
    });

// AutoMapper configuration
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

// Database configuration
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();  

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Exception handling middleware
app.UseGlobalExceptionHandler();

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