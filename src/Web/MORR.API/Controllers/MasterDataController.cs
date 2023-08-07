using MediatR;
using Microsoft.AspNetCore.Mvc;
using MORR.Application.Pipelines.Categories.Queries.GetCategoryMasterData;

namespace MORR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MasterDataController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("getCategoryMasterData")]
        public async Task<IActionResult> GetCategoryMasterData()
        {

            var response = await _mediator.Send(new GetCategoryMasterDataQuery());

            return Ok(response);
        }
    }
}
