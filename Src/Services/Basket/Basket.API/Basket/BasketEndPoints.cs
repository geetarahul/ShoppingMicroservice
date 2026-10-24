using Basket.API.Basket.DeleteBasket;
using Basket.API.Basket.GetBasket;
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket;
public static class BasketEndPoints
{
    public static void MapBasketEndpoints(this IEndpointRouteBuilder app)
    {
        DeleteBasketEndpoint.MapDeleteBasket(app);
        GetBasketEndpoints.MapGetBasket(app);
        StoreBasketEndpoints.MapStoreBasket(app);
    }
}