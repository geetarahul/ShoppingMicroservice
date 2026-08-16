using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.GetProducts;

//public record GetProductsQuery();
public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint
{
    public static void MapGetProducts(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            try
            {
                var result = await sender.Send(new GetProductQuery());

                var response = new ResultResponse<GetProductResponse>(new GetProductResponse(result.Products), System.Net.HttpStatusCode.OK, string.Empty, string.Empty);
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
        .WithName("GetProducts")
        .Produces<ResultResponse<GetProductResponse>>(StatusCodes.Status200OK)
        .Produces<ResultResponse<Unit>>(StatusCodes.Status500InternalServerError)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Retrieves a list of products");
    }
}
