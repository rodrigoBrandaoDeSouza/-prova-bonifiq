using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ServiceBase<T> where T : class
    {
        public readonly TestDbContext _ctx;

        public ServiceBase(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public EntityList<T> List(int page)
        {
            int pageSize = 10;

            var query = _ctx.Set<T>().AsQueryable();

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var entities = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new EntityList<T>
            {
                HasNext = page < totalPages,
                TotalCount = totalCount,
                List = entities
            };
        }
    }
}
