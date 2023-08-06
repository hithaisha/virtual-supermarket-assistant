using MediatR;
using MORR.Application.Common.Constants;
using MORR.Application.Common.Extentions;
using MORR.Application.DTOs.Common;
using MORR.Application.DTOs.ProductDTOs;
using MORR.Domain.Entities;
using MORR.Domain.Repositories.Command;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Products.Commads.SaveProduct
{
    public record SaveProductCommand(ProductDto productDetails) : IRequest<ResultDto>
    {
    }

    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, ResultDto>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductCommandRepository _productCommandRepository;
        public SaveProductCommandHandler(IProductQueryRepository productQueryRepository, IProductCommandRepository productCommandRepository)
        {
            this._productQueryRepository = productQueryRepository;
            this._productCommandRepository = productCommandRepository;
        }
        public async Task<ResultDto> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productQueryRepository
                             .GetById(request.productDetails.Id, cancellationToken);

                if(product is null)
                {
                    product = new Product();

                    product = request.productDetails.ToEntity();

                    var newProduct = await _productCommandRepository
                                    .AddAsync(product, cancellationToken);

                    return ResultDto
                           .Success(ResponseConstants.PRODUCT_SAVE_SUCCESS_RESPONSE_MESSAGE, newProduct.Id);
                }
                else
                {
                    product = request.productDetails.ToEntity(product);

                    await _productCommandRepository.UpdateAsync(product, cancellationToken);

                    return ResultDto.Success(ResponseConstants.PRODUCT_UPDATE_SUCCESS_RESPONSE_MESSAGE, product.Id);
                }


            }
            catch (Exception ex)
            {

                return ResultDto.Failure(new List<string>()
                {
                    ResponseConstants.COMMON_EXCEPTION_RESPONSE
                });
            }
        }
    }
}
