using API.Behaviors;
using Application.Features.Perspectives.Create; // For scanning validators & commands
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Optional: HttpClient if needed later
builder.Services.AddHttpClient();

// Database (scoped lifetime)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BSCDB")));

// Register repositories (scoped is best)
builder.Services.AddScoped<IPerspectiveRepository, PerspectiveRepository>();
// Add more repositories here later (OrgUnitRepository, etc.)

// MediatR - ONLY scan Application layer for handlers & commands/queries
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreatePerspectiveCommand).Assembly); // Application assembly only!
    
    // Optional: Add pipeline behaviors (validation, logging)
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// FluentValidation - scan validators from Application
builder.Services.AddValidatorsFromAssemblyContaining<CreatePerspectiveCommandValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Optional: Global validation exception handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (ValidationException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var errors = ex.Errors
            .Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage })
            .ToList();

        await context.Response.WriteAsJsonAsync(new { Errors = errors });
    }
});

app.MapControllers();

app.Run();