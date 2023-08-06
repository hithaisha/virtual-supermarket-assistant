using MORR.Domain.Entities;
using MORR.Domain.Repositories.Command;
using MORR.Infrastructure.Data;
using MORR.Infrastructure.Repositories.Command.Base;

namespace MORR.Infrastructure.Repositories.Command
{
    public class ProductCommandRepository 
            : CommandRepository<Product>, IProductCommandRepository
    {
        public ProductCommandRepository(MORRContext context)
            : base(context)
        {

        }
    }
}
