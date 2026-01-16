using API.Behaviors;
using Application.Common.Interfaces;
using Application.Features.Perspectives.Create; // for assembly scanning
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ────────────────────────────────────────────────
// Core Services
// ────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ────────────────────────────────────────────────
// Database
// ────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BSCDB")));

// ────────────────────────────────────────────────
// Repositories (scoped lifetime)
// ────────────────────────────────────────────────
builder.Services.AddScoped<IPerspectiveRepository, PerspectiveRepository>();

// ────────────────────────────────────────────────
// HttpClient + Repositories with SAP OData fixes
// ────────────────────────────────────────────────

// Common HttpClientHandler configuration for SAP (proxy bypass + self-signed cert + TLS)
var sapHandler = new HttpClientHandler
{
    UseProxy = false,                                      // Bypass corporate proxy
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator, // DEV ONLY!
    SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13
};

// Employee repository + client
builder.Services.AddHttpClient<IEmployeeRepository, EmployeeRepository>(client =>
{
    client.BaseAddress = new Uri("https://eepers02.eep.com.et:8001/sap/opu/odata/sap/ZHR_ORGSTRUCT_DATA_SRV/");
    client.Timeout = TimeSpan.FromSeconds(90);
}).ConfigurePrimaryHttpMessageHandler(() => sapHandler);

// OrgUnit repository + client (same fixes)
builder.Services.AddHttpClient<IOrgUnitRepository, OrgUnitRepository>(client =>
{
    client.BaseAddress = new Uri("https://eepers02.eep.com.et:8001/sap/opu/odata/sap/ZHR_ORGSTRUCT_DATA_SRV/");
    client.Timeout = TimeSpan.FromSeconds(90);
}).ConfigurePrimaryHttpMessageHandler(() => sapHandler);

// ────────────────────────────────────────────────
// MediatR + Behaviors
// ────────────────────────────────────────────────
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreatePerspectiveCommand).Assembly);

    // Your validation behavior
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreatePerspectiveCommandValidator>();

// ────────────────────────────────────────────────
var app = builder.Build();

// ────────────────────────────────────────────────
// Pipeline
// ────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global validation exception handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (FluentValidation.ValidationException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var errors = ex.Errors.Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage });
        await context.Response.WriteAsJsonAsync(new { Errors = errors });
    }
});

app.MapControllers();

app.Run();