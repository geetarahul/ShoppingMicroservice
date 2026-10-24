namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoints
{
    public static void MapStoreBasket(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
           var command = new StoreBasketCommand(request.Cart);

           var result = await sender.Send(command);

            var response = new StoreBasketResponse(request.Cart.UserName);

            return Results.Created($"/basket/{response.UserName}",
                new ResultResponse<StoreBasketResponse>(
                    response,
                    System.Net.HttpStatusCode.Created,
                    null,
                    null
                )
            );
        })
        .WithName("StoreBasket")
        .Produces<ResultResponse<StoreBasketResponse>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store basket")
        .WithDescription("Store basket");
    }
}