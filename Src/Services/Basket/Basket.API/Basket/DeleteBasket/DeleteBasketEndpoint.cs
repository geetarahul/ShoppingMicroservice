namespace Basket.API.Basket.DeleteBasket;
public record DeleteBasketResponse(bool IsSuccess);

public static class DeleteBasketEndpoint
{
    public static void MapDeleteBasket(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var command = new DeleteBasketCommand(userName);
            var result = await sender.Send(command);

            return Results.Ok(new ResultResponse<DeleteBasketResponse>(
                new DeleteBasketResponse(result.IsSuccess), 
                System.Net.HttpStatusCode.Created, 
                null, 
                null));
        })
        .WithName("DeleteBasket")
        .Produces<ResultResponse<DeleteBasketResponse>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete basket")
        .WithDescription("Delete basket");
    }
}