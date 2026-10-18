using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductQuery(int? pageNumber = 1, int? pageSize = 10) : IQuery<GetProductResults>;
    public record GetProductResults(IEnumerable<Product> Products);

    public class GetProductHandler(IDocumentSession session) 
        : IQueryHandler<GetProductQuery, GetProductResults>
    {
        public async Task<GetProductResults> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.pageNumber ?? 1, query.pageSize ?? 5, cancellationToken);

            return new GetProductResults(products);
        }
    }
}
