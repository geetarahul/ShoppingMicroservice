using Catalog.API.Products;
using FluentValidation; 
using Marten;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient(provider =>
    provider.GetRequiredService<ILoggerFactory>().CreateLogger("Default"));
builder.Services.AddOpenApi();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("Marten")!);
}).UseLightweightSessions();
// Add services to the container.

var app = builder.Build();
if (!app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
// Configure the HTTP request pipeline.
app.MapProductEndpoints();
app.Run();
