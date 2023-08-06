using MediatR;
using MORR.Application.DTOs.Common;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Categories.Queries.GetCategoryMasterData
{
    public record GetCategoryMasterDataQuery : IRequest<List<DropDownDto>>
    { 
    }

    public class GetCategoryMasterDataQueryHandler : IRequestHandler<GetCategoryMasterDataQuery, List<DropDownDto>>
    {
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        public GetCategoryMasterDataQueryHandler(ICategoryQueryRepository categoryQueryRepository)
        {
            this._categoryQueryRepository = categoryQueryRepository;
        }
        public async Task<List<DropDownDto>> Handle(GetCategoryMasterDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var listOfCategory = (await _categoryQueryRepository
                                    .GetAll(cancellationToken))
                                    .Select(x=> new DropDownDto()
                                    {
                                        Id = x.Id,
                                        Name = x.Name,

                                    }).ToList();

                return listOfCategory;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
