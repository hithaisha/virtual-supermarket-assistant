using MediatR;
using MORR.Application.Common.Extentions;
using MORR.Application.DTOs.ProductDTOs;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQuery : IRequest<List<ProductDto>>
    {
        public string SearchText { get; set; }
        public int CategoryId { get; set; }
    }

    public class GetProductsByFilterQueryHandler : IRequestHandler<GetProductsByFilterQuery, List<ProductDto>>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        public GetProductsByFilterQueryHandler(IProductQueryRepository productQueryRepository)
        {
            this._productQueryRepository = productQueryRepository;
        }
        public async Task<List<ProductDto>> Handle(GetProductsByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new List<ProductDto>();

                var listOfProducts = await _productQueryRepository.Query(x=>x.IsDeleted == false && x.IsActive == true);

                if(!string.IsNullOrEmpty(request.SearchText))
                {
                    listOfProducts = listOfProducts
                                     .Where(x => x.ItemName.Contains(request.SearchText))
                                     .OrderByDescending(x => x.CreatedDate);
                }

                if(request.CategoryId > 0)
                {
                    listOfProducts = listOfProducts
                                    .Where(x=>x.CategoryId == request.CategoryId)
                                    .OrderByDescending(x => x.CreatedDate);
                }

                foreach(var product in listOfProducts.ToList())
                {
                    response.Add(product.ToDto());
                }

                return response;
            }
            catch (Exception ex)
            {
                return new List<ProductDto>();
            }
        }
    }
}
