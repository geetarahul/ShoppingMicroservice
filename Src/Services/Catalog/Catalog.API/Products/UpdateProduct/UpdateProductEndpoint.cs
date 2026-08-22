using Catalog.API.Models;
using MediatR;
using Catalog.API.Helper;
using System.Net;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string? Name, List<string> Category, string? Description, string? ImageFile, decimal Price);
public record UpdateProductResponse(bool IsSuccess);

internal class UpdateProductEndpoint
{
    public static void MapUpdateProductEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            try
            {
                var command = request.MapToCommand();
                var result = await sender.Send(command);
                var response = new ResultResponse<UpdateProductResponse>(Data: new UpdateProductResponse(result.IsSuccess), StatusCode: HttpStatusCode.OK);
                return Results.Ok(response);
            }
            catch(Exception ex) {
                var response = new ResultResponse<Unit>(Unit.Value, StatusCode: HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);
                return Results.InternalServerError(response);
            }
        })
        .WithName("UpdateProduct")
        .WithTags("Products")
        .Produces<ResultResponse<UpdateProductResult>>(StatusCodes.Status200OK)
        .Produces<ResultResponse<Unit>>(StatusCodes.Status404NotFound)
        .Produces<ResultResponse<Unit>>(StatusCodes.Status500InternalServerError)
        .ProducesProblem(StatusCodes.Status400BadRequest);

    }
}
