using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

public class ProductNotFoundExecption : NotFoundException
{
    public ProductNotFoundExecption(Guid Id) : base("Product", Id)
    {
    }
}
