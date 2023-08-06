using MORR.Domain.Entities;
using MORR.Domain.Repositories.Query;
using MORR.Infrastructure.Data;
using MORR.Infrastructure.Repositories.Query.Base;

namespace MORR.Infrastructure.Repositories.Query
{
    public class ProductQueryRepository : QueryRepository<Product>, IProductQueryRepository
    {
        public ProductQueryRepository(MORRContext context)
            : base(context)
        {

        }

    }
}
