using Catalog.API.Exceptions;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint
{    
    public static void MapGetProductByIdEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                return Results.Ok(new ResultResponse<GetProductByIdResult>(result, System.Net.HttpStatusCode.OK, null, null));

            }
            catch (ProductNotFoundExecption ex)
            {
                var response = new ResultResponse<Unit>(
                    Unit.Value,
                    System.Net.HttpStatusCode.NotFound,
                    ex.Message,
                    ex.StackTrace ?? string.Empty);
                return Results.NotFound(response);
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
        .WithName("GetProductById")
        .Produces<ResultResponse<GetProductByIdResult>>(StatusCodes.Status200OK)
        .Produces<ResultResponse<Unit>>(StatusCodes.Status500InternalServerError)
        .Produces<ResultResponse<Unit>>(StatusCodes.Status404NotFound)
        .WithSummary("Get Product By Id")
        .WithDescription("Retrieves a product by its unique identifier");
    }
}
