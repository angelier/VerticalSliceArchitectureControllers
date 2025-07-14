using Application.Infrastructure.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Domain.Entities;

namespace Application.Features.Products.Queries
{
    public class GetProductById
    {
        public record GetProductByIdRequest(int Id) : IRequest<GetProductByIdResponse>;
        public record GetProductByIdResponse(int ProductId, string Name, string Description, double Price, string CategoryName);
    
    
        public class GetProductByIdHandler(ApiDbContext context, IMapper mapper) : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
        {
            public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
            {
                var product = await context.Products
                    .Where(p => p.ProductId == request.Id)
                    .ProjectTo<GetProductByIdResponse>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                return product;
            }
        }

        public class GetProductByIdMappingProfile : Profile
        {
            public GetProductByIdMappingProfile() => CreateMap<Product, GetProductByIdResponse>()
                .ForMember(
                    d => d.CategoryName,
                    opt => opt.MapFrom(mf => mf.Category != null ? mf.Category.Name : string.Empty)
                );
        }

    }
}