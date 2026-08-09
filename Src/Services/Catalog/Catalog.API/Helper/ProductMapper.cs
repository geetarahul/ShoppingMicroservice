using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Helper
{
    public static class ProductMapper
    {
        public static CreateProductCommand MapToCommand(this CreateProductRequest request) => new CreateProductCommand(
            request.Name,
            request.Category,
            request.Description,
            request.ImageFile,
            request.Price);
    }
}
