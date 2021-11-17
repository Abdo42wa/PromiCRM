using Microsoft.EntityFrameworkCore;
using PromiCRM.IRepository;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Repository
{
    public class GenericRepository<T> :IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> expression, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;

            if (include != null)
            {
                //applying include to query
                query = include(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;
            //check if there was expression. like we want to get list of Products
            //where product name is Nike sss. Then Filter query for me please
            if (expression != null)
            {
                query = query.Where(expression);
            }

            //like we would want when getting Product also include Brand object. filling our Brand
            //property in Country object field. we then will inlude to query whatever properties that were asked for
            //if in includes list you have added five foreign keys it will loop five times
            if (include != null)
            {
                query = include(query);
            }
            //then order if neccessary. like person put Dessending or accending
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            //but we probably dont want to include something all the time for speed purposes
            //AsNoTracking means any record that is retrieved with this function is not tracked
            //its send to client and entity framework doesnt care about it
            //expression means that it allows us to put LAMBDA expression like h => h.Id = id, its basically condition(bool)
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
