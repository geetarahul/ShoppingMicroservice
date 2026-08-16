using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductQuery() : IQuery<GetProductResults>;
    public record GetProductResults(IEnumerable<Product> Products);

    public class GetProductHandler(IDocumentSession session, ILogger logger) 
        : IQueryHandler<GetProductQuery, GetProductResults>
    {
        public async Task<GetProductResults> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductResults(products);
        }
    }
}
