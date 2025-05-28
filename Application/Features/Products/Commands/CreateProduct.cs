using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Application.Domain.Entities;
using Application.Infrastructure.Persistence;

namespace Application.Features.Products.Commands;

public class CreateProduct
{
    public record CreateProductCommand(string Name, string Description, double Price, int CategoryId) : IRequest<IResult>;
   
    public class CreateProductCommandtValidator : AbstractValidator<CreateProductCommand>{
        public CreateProductCommandtValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Product description is required.");
            RuleFor(r => r.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
            RuleFor(r => r.CategoryId).GreaterThan(0).WithMessage("Category ID must be a valid positive integer.");
        }
    }

    public class CreateProductHandler(ApiDbContext context, IValidator<CreateProductCommand> validator) : IRequestHandler<CreateProductCommand, IResult>
    {
        public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var validationProblems = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                return Results.ValidationProblem(validationProblems);
            }

            var newProduct = new Product(0, request.Name, request.Description, request.Price, request.CategoryId);

            context.Products.Add(newProduct);

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created($"api/products/{newProduct.ProductId}", null);
        }
    }

}