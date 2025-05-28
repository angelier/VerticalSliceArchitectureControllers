using MediatR;
using Microsoft.AspNetCore.Http;
using Application.Infrastructure.Persistence;

namespace Application.Features.Products.Commands;

public class DeleteProduct 
{
    public class DeleteProductCommand(int productId) : IRequest<IResult>
    {
        public int ProductId { get; set; } = productId;
    }

    public class DeleteProductHandler(ApiDbContext context) : IRequestHandler<DeleteProductCommand, IResult>
    {
        public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(request.ProductId);

            if (product is null)
            {
                return Results.NotFound();
            }

            context.Products.Remove(product);

            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}