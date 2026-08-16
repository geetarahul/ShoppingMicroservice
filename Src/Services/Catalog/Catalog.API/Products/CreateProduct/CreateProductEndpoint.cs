using Catalog.API.Helper;
using Catalog.API.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public class CreateProductEndpoint
{
    public static void MapCreateProduct(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.MapToCommand();
            try
            {
                var result = await sender.Send(command);

                var response = new ResultResponse<CreateProductResult>(
                    result,
                    System.Net.HttpStatusCode.Created,
                    string.Empty,
                    string.Empty);

                return Results.Created($"/products/{result.Id}", response);
            }
            catch (Exception ex)
            {
                var response = new ResultResponse<Unit>(
                    Unit.Value,
                    System.Net.HttpStatusCode.InternalServerError,
                    ex.Message,
                    ex.StackTrace ?? string.Empty);
                return Results.InternalServerError(response);
            }
        })
        .WithName("CreateProduct")
        .Produces<ResultResponse<CreateProductResult>>(StatusCodes.Status201Created)
        .Produces<ResultResponse<Unit>>(statusCode: StatusCodes.Status500InternalServerError)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Creates a new product");
    }
}
