using Kanayri.Application.Commands;
using Kanayri.Application.Models;
using Kanayri.Domain.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
