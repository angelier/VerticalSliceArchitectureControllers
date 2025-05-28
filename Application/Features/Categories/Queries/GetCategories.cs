using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Domain.Entities;
using Application.Infrastructure.Persistence;

namespace Application.Features.Categories.Queries;

public class GetCategories 
{
    public class GetCategoriesQuery : IRequest<List<GetCategoriesResponse>>
    {

    }

    public class GetCategoriesHandler(ApiDbContext context, IMapper mapper)
        : IRequestHandler<GetCategoriesQuery, List<GetCategoriesResponse>>
    {
        public Task<List<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) =>
            context.Categories.ProjectTo<GetCategoriesResponse>(mapper.ConfigurationProvider).ToListAsync();
    }

    public class GetCategoriesResponse
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }

    public class GetCategoriesMappingProfile : Profile
    {
        public GetCategoriesMappingProfile() => CreateMap<Category, GetCategoriesResponse>();
    }
    
}
