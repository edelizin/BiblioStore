using BiblioDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BiblioDomain.Models;
using BiblioInfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiblioInfrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly BiblioStoreDbContext Db;

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(BiblioStoreDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }
        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
           await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Search(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }
    }
}
