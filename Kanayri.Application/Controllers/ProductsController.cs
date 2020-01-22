using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Kanayri.Application.Models;
using Kanayri.Domain.Product;
using Kanayri.Domain.Product.Commands;
using Kanayri.Domain.Product.Queries;

namespace Kanayri.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            if (!Guid.TryParse(id, out var guidId))
            {
                return BadRequest();
            }

            var command = new ProductGetQuery(guidId);
            var result = await _mediator.Send(command);

            return result != null ? (ActionResult<Product>) Ok(result) : NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] ProductCreateDto product)
        {
            var command = new ProductCreateCommand
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = product.Price
            };

            return Ok(await _mediator.Send(command));
        }
    }
}
