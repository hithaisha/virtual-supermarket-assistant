using MediatR;
using MORR.Application.Common.Constants;
using MORR.Application.DTOs.Common;
using MORR.Domain.Repositories.Command;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Products.Commads.DeleteProduct
{
    public record DeleteProductCommand(int id) : IRequest<ResultDto>;

    public class DeleteProductCommandHander : IRequestHandler<DeleteProductCommand, ResultDto>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductCommandRepository _productCommandRepository;
        public DeleteProductCommandHander(IProductQueryRepository productQueryRepository, IProductCommandRepository productCommandRepository)
        {
            this._productQueryRepository = productQueryRepository;
            this._productCommandRepository = productCommandRepository;
        }
        public async Task<ResultDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productQueryRepository.GetById(request.id, cancellationToken);

                if (product is null) 
                {
                    return ResultDto.Failure(new List<string>
                    {
                        ResponseConstants.PRODUCT_NOT_AVAILABLE_RESPONSE_MESSAGE
                    });
                }
                else
                {
                    product.IsDeleted = true;

                    await _productCommandRepository.UpdateAsync(product, cancellationToken);

                    return ResultDto.Success(ResponseConstants.PRODUCT_DELETE_SUCCESS_RESPONSE_MESSAGE);
                }
            }
            catch (Exception ex)
            {

                return ResultDto.Failure(new List<string>
                {
                        ResponseConstants.COMMON_EXCEPTION_RESPONSE
                });
            }
        }
    }


}
