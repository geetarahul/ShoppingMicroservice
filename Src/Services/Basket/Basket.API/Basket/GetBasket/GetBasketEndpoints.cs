namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints
{
    public static void MapGetBasket(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));            
            var response = new GetBasketResponse(result.Cart);

            return Results.Ok(
                new ResultResponse<GetBasketResponse>(
                    response, 
                    System.Net.HttpStatusCode.OK, 
                    null, 
                    null));
        })
        .WithName("GetProductByUserName")
        .Produces<ResultResponse<GetBasketResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get product by username")
        .WithDescription("Get product by username");
    }
}
