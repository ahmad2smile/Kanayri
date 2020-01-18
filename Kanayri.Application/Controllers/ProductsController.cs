using Kanayri.Application.Commands.Products;
using Kanayri.Application.Models;
using Kanayri.Application.Queries.Products;
using Kanayri.Domain.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            if (Guid.TryParse(id, out Guid guidId))
            {
                var command = new ProductQuery(guidId);
                var result = await _mediator.Send(command);

                return result != null ? (ActionResult<Product>) Ok(result) : NotFound();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] ProductCreateDto createDto)
        {
            var command = new ProductCreateCommand
            {
                Name = createDto.Name
            };

            return Ok(await _mediator.Send(command));
        }
    }
}
