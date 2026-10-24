using Basket.API.Basket;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Marten;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services.AddTransient(provider =>
    provider.GetRequiredService<ILoggerFactory>().CreateLogger("Default"));
builder.Services.AddOpenApi();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapBasketEndpoints();
app.UseExceptionHandler(options => { });
app.Run();