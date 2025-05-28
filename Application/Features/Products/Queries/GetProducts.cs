using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Domain.Entities;
using Application.Infrastructure.Persistence;

namespace Application.Features.Products.Queries;
public class GetProducts
{
    public record GetProductsQuery() : IRequest<IEnumerable<GetProductsResponse>>;
    public record GetProductsResponse(int ProductId, string Name, string Description, double Price, string CategoryName);


    public class GetProductsHandler(ApiDbContext context, IMapper mapper): IRequestHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
    {
        public Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken) {
            
            var response = context.Products.ProjectTo<GetProductsResponse>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken); 

            return response.ContinueWith(task => task.Result.AsEnumerable(), cancellationToken);
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