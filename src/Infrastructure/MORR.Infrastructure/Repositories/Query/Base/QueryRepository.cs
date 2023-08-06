using Microsoft.EntityFrameworkCore;
using MORR.Domain.Repositories.Query.Base;
using MORR.Infrastructure.Data;
using System.Linq.Expressions;

namespace MORR.Infrastructure.Repositories.Query.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected readonly MORRContext _context;
        private DbSet<T> _entities;

        public QueryRepository(MORRContext context)
        {
            this._context = context;
            this._entities = context.Set<T>();
        }
        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _entities.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _entities.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<IQueryable<T>> Query(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }
    }
}
