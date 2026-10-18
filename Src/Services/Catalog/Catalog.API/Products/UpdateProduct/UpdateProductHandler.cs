using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using FluentValidation;
using Marten;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string? Name, List<string> Category, string? Description, string? ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.").Length(2, 150).WithMessage("Product Name must be between 2 and 150 characters.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product Price must be greater than 0.");
    }
}

public class UpdateProductHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
            throw new ProductNotFoundExecption(command.Id);


        product.Name = command.Name ?? product.Name;
        product.Category = command.Category ?? product.Category;
        product.Description = command.Description ?? product.Description;
        product.ImageFile = command.ImageFile ?? product.ImageFile;
        product.Price = command.Price != default ? command.Price : product.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
