using BuildingBlocks.CQRS;
using Catalog.API.Models;
using System.Net;
using System.Windows.Input;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Create product entity
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Category = command.Category,
            Price = command.Price,
        };
        product.Id = Guid.NewGuid();
        //save to database

        //return createproductresult result
        var response = new CreateProductResult(product.Id);
        return Task.FromResult(response);
    }
}
