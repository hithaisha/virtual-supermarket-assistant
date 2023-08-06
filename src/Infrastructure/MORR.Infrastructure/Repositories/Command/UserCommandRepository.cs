using MORR.Domain.Entities;
using MORR.Domain.Repositories.Command;
using MORR.Infrastructure.Data;
using MORR.Infrastructure.Repositories.Command.Base;

namespace MORR.Infrastructure.Repositories.Command
{
    public class UserCommandRepository : CommandRepository<User>, IUserCommandRepository
    {
        public UserCommandRepository(MORRContext context)
            : base(context)
        {

        }
    }
}
