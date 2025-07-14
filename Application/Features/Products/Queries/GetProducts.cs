using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Domain.Entities;
using Application.Infrastructure.Persistence;

namespace Application.Features.Products.Queries;
public class GetProducts
{
    public record GetProductsRequest() : IRequest<IEnumerable<GetProductsResponse>>;
    public record GetProductsResponse(int ProductId, string Name, string Description, double Price, string CategoryName);

    public class GetProductsHandler(ApiDbContext context, IMapper mapper): IRequestHandler<GetProductsRequest, IEnumerable<GetProductsResponse>>
    {
        public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await context.Products
                .ProjectTo<GetProductsResponse>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return products;
        }
            
    }

    public class GetProductsMappingProfile : Profile
    {
        public GetProductsMappingProfile() => CreateMap<Product, GetProductsResponse>()
            .ForMember(
                d => d.CategoryName,
                opt => opt.MapFrom(mf => mf.Category != null ? mf.Category.Name : string.Empty)
            );
    }

  
}