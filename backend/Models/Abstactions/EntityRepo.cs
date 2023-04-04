using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Contexts;

namespace backend.Models
{
    public class EntityRepo<TEntity> : IEntityRepo<TEntity>
    where TEntity : BaseEntity
{
    protected readonly KinderContext _context;
    public EntityRepo(KinderContext context)
    {
        _context = context;
    }
    public virtual async Task<TEntity> Add(TEntity entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
    public virtual async Task<IQueryable<TEntity>> GetAll()
    {
        return _context.Set<TEntity>();
    }
    public virtual async Task<TEntity> GetById(long id)
    {
      var item = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
      return item;
    }
     public virtual async Task Remove(long id)
    {
        var item = await GetById(id);
        _context.Remove(item);

        await _context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        //var item = await GetById(id);
        //item = entity;
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}
}