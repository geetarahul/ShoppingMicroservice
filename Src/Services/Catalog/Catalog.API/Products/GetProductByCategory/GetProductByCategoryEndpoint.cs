using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

internal class GetProductByCategoryEndpoint
{
    public static void MapGetProductByCategoryEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            try
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));

                var response = new ResultResponse<GetProductByCategoryResponse>(new GetProductByCategoryResponse(result.Products), System.Net.HttpStatusCode.OK, string.Empty, string.Empty);
                return Results.Ok(response);
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
        .WithName("GetProductByCategory")
        .Produces<ResultResponse<GetProductByCategoryResponse>>(StatusCodes.Status200OK)
        .Produces<ResultResponse<Unit>>(StatusCodes.Status500InternalServerError)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products by Category")
        .WithDescription("Retrieves a list of products filtered by category");
    }
}
