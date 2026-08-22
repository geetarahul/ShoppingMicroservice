using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.UpdateProduct;

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


        public static UpdateProductCommand MapToCommand(this UpdateProductRequest request) => new UpdateProductCommand(
            request.Id,
            request.Name,
            request.Category,
            request.Description,
            request.ImageFile,
            request.Price);
    }
}
