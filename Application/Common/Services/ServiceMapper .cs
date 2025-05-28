using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;

namespace Application.Common.Services
{
    public class ServiceMapper(TypeAdapterConfig config) : IMapper
    {
        private readonly TypeAdapterConfig _config = config;

        public TypeAdapterConfig Config => throw new NotImplementedException();

        public ITypeAdapterBuilder<TSource> From<TSource>(TSource source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>(_config);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TSource, TDestination>(_config);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}