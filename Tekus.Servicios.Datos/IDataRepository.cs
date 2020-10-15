using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tekus.Entities;

namespace Tekus.Servicios.Datos
{
    public interface IDataRepository<TEntity> where TEntity : EntidadBase
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
