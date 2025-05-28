using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Features.Products.Queries.GetProducts;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController (IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private static readonly List<string> Products = new List<string>
        {
            "Product1",
            "Product2",
            "Product3"
        };

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [EndpointDescription("Retrieves all products in the system.")]
        [Produces("application/json")]  
        public async Task<ActionResult<IEnumerable<string>>> GetProductsAsync()
        {

            var response = await _mediator.Send(new GetProductsQuery());
            return Ok(Products);
        }

        [HttpGet("{id}")]
        [EndpointDescription("Retrieves product by their unique identifier.")]
        [Produces("application/json")]    
        [ProducesResponseType(404)]
        public ActionResult<string> GetProduct(int id)
        {
            if (id < 0 || id >= Products.Count)
            {
                return NotFound();
            }
            return Ok(Products[id]);
        }
    }
}