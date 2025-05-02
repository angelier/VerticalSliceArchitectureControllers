using Application.Domain.Entities;

namespace Application.Domain.Events;

 public class ProductUpdatePriceEvent(Product product) : DomainEvent
{
    public Product Product { get; set; } = product;
}