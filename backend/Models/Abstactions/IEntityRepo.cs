using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public interface IEntityRepo<TEntity>
    {
      Task<TEntity> Add(TEntity entity);
      Task<TEntity> GetById(long id);
      Task Remove(long id);
      Task<TEntity> Update(TEntity entity);
      Task<IQueryable<TEntity>> GetAll();
    }
}