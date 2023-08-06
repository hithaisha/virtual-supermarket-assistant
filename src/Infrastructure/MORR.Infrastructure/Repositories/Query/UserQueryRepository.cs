using MORR.Domain.Entities;
using MORR.Domain.Repositories.Query;
using MORR.Infrastructure.Data;
using MORR.Infrastructure.Repositories.Query.Base;

namespace MORR.Infrastructure.Repositories.Query
{
    public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
    {
        public UserQueryRepository(MORRContext context)
         : base(context)
        {

        }

    }
}
