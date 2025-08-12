using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    // Constraints =>Kısıtlar, kullanıcı istediği tipi versin fakat sınırlar içerisinde.
    // TContext tipi kullanıcı tarafından belirlendiği için DbContext olarak belirlenmiştir. Amaç, bağımlılığı azaltmak.
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity> ,IAsyncRepository<TEntity> where TContext : DbContext where TEntity : Entity
    {
        private readonly TContext Context;

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }


        public void Update(TEntity entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        // OrderBy
        public List<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> data = Context.Set<TEntity>();

            if (predicate != null)
                data = data.Where(predicate);

            return data.ToList();
        }

        // OrderBy
        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> data = Context.Set<TEntity>();

            return data.FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            IQueryable<TEntity> data = Context.Set<TEntity>();

            return await data.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> data = Context.Set<TEntity>();

            if (predicate != null)
                data = data.Where(predicate);

            return await data.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}

// DipNot: Update ve Delete'de komut hazırlanır, çalıştırılır ama Save işlemi olana kadar veritabanına gitmez. Böylece aslında veritabanına istek atmış olmaz. İstek atmadığı için bunun asenkron olması gerekmez, veritabanı işlemi değil. Çünkü veritabanındaki bekleme süresini sağlamaz.

// Update ve Delete metotlarında sadece SaveChanges kısımlarının asenkron yapılması yeterlidir.
