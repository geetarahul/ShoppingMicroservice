using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products
{
    public static class MapProductEndPoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            // Delegate registration to individual endpoint classes
            CreateProductEndpoint.MapCreateProduct(app);
        }
    }
}
