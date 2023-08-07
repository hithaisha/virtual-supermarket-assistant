using MediatR;
using MORR.Application.Common.Constants;
using MORR.Application.DTOs.Common;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Products.Queries.ItemCodeValidator
{
    public record ItemCodeValidatorQuery(string itemCode) : IRequest<ResultDto>;


    public class ItemCodeValidatorQueryHandler : IRequestHandler<ItemCodeValidatorQuery, ResultDto>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        public ItemCodeValidatorQueryHandler(IProductQueryRepository productQueryRepository)
        {
            this._productQueryRepository = productQueryRepository;
        }
        public async Task<ResultDto> Handle(ItemCodeValidatorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = (await _productQueryRepository.Query(x=>x.ItemCode == request.itemCode))
                              .FirstOrDefault();

                if (product is null)
                {
                    return ResultDto.Success(string.Empty);
                }
                else
                {
                    return ResultDto.Failure(new List<string>()
                    {
                        ResponseConstants.PRODUCT_ITEM_CODE_ALLREADY_EXISTS_RESPONSE_MESSAGE
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
