using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint
{
    public static void MapDeleteProductEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                var response = new ResultResponse<DeleteProductResponse>(
                    new DeleteProductResponse(result.Success),
                    System.Net.HttpStatusCode.OK,
                    string.Empty,
                    string.Empty);
                return Results.Ok(response);
            }
            catch(Exception ex)
            {
                var response = new ResultResponse<Unit>(
                    Unit.Value,
                    System.Net.HttpStatusCode.InternalServerError,
                    ex.Message,
                    ex.StackTrace ?? string.Empty);
                return Results.InternalServerError(response);
            }
        })
        .WithName("DeleteProduct")
        .WithTags("Products")
        .Produces<ResultResponse<DeleteProductResponse>>(StatusCodes.Status200OK)
        .Produces<ResultResponse<Unit>>(StatusCodes.Status500InternalServerError)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
