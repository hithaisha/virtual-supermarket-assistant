using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MORR.Application.DTOs.ProductDTOs;
using MORR.Application.Pipelines.Products.Commads.DeleteProduct;
using MORR.Application.Pipelines.Products.Commads.SaveProduct;
using MORR.Application.Pipelines.Products.Queries.GetProductsByFilter;
using MORR.Application.Pipelines.Products.Queries.ItemCodeValidator;

namespace MORR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("saveProduct")]
        public async Task<IActionResult> SaveProduct([FromBody] ProductDto dto)
        {
            var response = await _mediator.Send(new SaveProductCommand(dto));

            return Ok(response);
        }

        [HttpPost("validateItemCode")]
        public async Task<IActionResult> ValidateItemCode([FromBody] string itemCode)
        {
            var response = await _mediator.Send(new ItemCodeValidatorQuery(itemCode));

            return Ok(response);
        }

        [HttpPost("getProductsByFilter")]
        public async Task<IActionResult> GetProductsByFilter([FromBody] GetProductsByFilterQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _mediator.Send(new DeleteProductCommand(id));

            return Ok(response);
        }

    }
}
