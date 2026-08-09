using Catalog.API.Products;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
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
