namespace Catalog.API.Exceptions
{
    public class ProductNotFoundExecption : Exception
    {
        public ProductNotFoundExecption() : base("Product not found!")
        {
        }
    }
}
