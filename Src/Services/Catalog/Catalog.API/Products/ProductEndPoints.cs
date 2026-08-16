using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products
{
    public static class MapProductEndPoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            // Delegate registration to individual endpoint classes
            CreateProductEndpoint.MapCreateProduct(app);
            GetProductsEndpoint.MapGetProducts(app);
            GetProductByIdEndpoint.MapGetProductByIdEndpoint(app);
            GetProductByCategoryEndpoint.MapGetProductByCategoryEndpoint(app);
        }
    }
}
