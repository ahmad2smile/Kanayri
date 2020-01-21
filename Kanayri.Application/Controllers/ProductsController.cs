using Kanayri.Application.Commands.Products;
using Kanayri.Application.Models;
using Kanayri.Domain.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Queries;
using Kanayri.Persistence;

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
        public async Task<ActionResult<ProductModel>> GetProduct(string id)
        {
            if (!Guid.TryParse(id, out var guidId))
            {
                return BadRequest();
            }

            var command = new ProductGetQuery(guidId);
            var result = await _mediator.Send(command);

            return result != null ? (ActionResult<ProductModel>) Ok(result) : NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProduct([FromBody] ProductCreateDto createDto)
        {
            var command = new ProductCreateCommand
            {
                Name = createDto.Name
            };

            return Ok(await _mediator.Send(command));
        }
    }
}
