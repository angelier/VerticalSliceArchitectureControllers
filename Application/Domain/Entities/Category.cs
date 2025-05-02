using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Domain.Entities;

public class Category(int categoryId, string name)
{
    public int CategoryId { get; set; } = categoryId;
    public string Name { get; set; } = name;

    public ICollection<Product> Products { get; set; } =
        new HashSet<Product>();
}