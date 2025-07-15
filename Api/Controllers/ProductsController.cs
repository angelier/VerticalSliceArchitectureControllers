using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Features.Products.Queries.GetProductById;
using static Application.Features.Products.Queries.GetProducts;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController (IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [EndpointDescription("Retrieves all products in the system.")]
        [Produces("application/json")]  
        public async Task<ActionResult<IEnumerable<string>>> GetProductsAsync()
        {

            var products = await _mediator.Send(new GetProductsRequest());
            return Ok(products);
        }

        [HttpGet("{id}")]
        [EndpointDescription("Retrieves product by their unique identifier.")]
        [Produces("application/json")]    
        [ProducesResponseType(404)]
        public async Task<ActionResult<string>> GetProduct(int id)
        {
            var product = await _mediator.Send(new GetProductByIdRequest(id));
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [EndpointDescription("Retrieves all products in the system.")]
        [Produces("application/json")]
        public void  GetPrueba()
        {



            var foo = 1;

            switch (foo)
            {
                                    case 1: Console.WriteLine("Case 1 executed");
            break;
                case 2:
                                           Console.WriteLine("Case 2 executed");
                    break;
             case 3:
                             Console.WriteLine("Case 3 executed");
                    break;
                default:
                    break;
            }
        }
    }
}
