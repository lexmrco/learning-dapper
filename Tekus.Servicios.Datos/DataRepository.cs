using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekus.Entities;

namespace Tekus.Servicios.Datos
{
    public abstract class DataRepository<TEntity> : IDataRepository<TEntity> where TEntity : EntidadBase
    {
        readonly TekusDbContext _nexosDbContext;
        public DataRepository(TekusDbContext context)
        {
            _nexosDbContext = context;
        }
        public abstract DbSet<TEntity> Dbset { get; }

        public virtual void Add(TEntity entity)
        {
            Dbset.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Dbset.Remove(entity);
        }

        public virtual TEntity Get(int id)
        {
            return Dbset.FirstOrDefault(e => e.ID == id);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await Dbset.FirstOrDefaultAsync(e => e.ID == id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Dbset.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Dbset.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            Dbset.Update(entity);
        }

        public void SaveChanges()
        {
            _nexosDbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _nexosDbContext.SaveChangesAsync();
        }
    }
}
