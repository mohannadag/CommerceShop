using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Core;
using Commerce.Core.Common;

namespace Commerce.Data
{
    public class EntityRepo<TEntity> : IEntityRepo<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<TEntity> table;

        public EntityRepo(ApplicationDbContext context)
        {
            this.context = context;
            table = context.Set<TEntity>();
        }
        public virtual TEntity Add(TEntity entity)
        {
            //entity.Guid = Guid.NewGuid();
            table.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            //entity.Guid = Guid.NewGuid();
            table.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public void Delete(Guid guid)
        {
            TEntity entity = table.FirstOrDefault(t => t.Guid == guid);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            table.Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteAsync(Guid guid)
        {
            TEntity entity = await table.FirstOrDefaultAsync(t => t.Guid == guid);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            table.Remove(entity);
            await context.SaveChangesAsync();
            
        }

        public async Task DeleteEntityAsync(TEntity entity)
        {
            
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            table.Remove(entity);
            await context.SaveChangesAsync();

        }

        public async Task DeleteAsync(TEntity entity)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));

                case ISoftDeletedEntity softDeletedEntity:
                    softDeletedEntity.Deleted = true;
                    await UpdateAsync(entity);
                    break;

                default:
                    await DeleteEntityAsync(entity);
                    break;
            }

        }

        //public Task DeleteAsync(TEntity entity)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<TEntity> GetAll()
        {
            return table;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = true, string culture = null)
        {
            IEnumerable<TEntity> output = table;
            try
            {
                await Task.Run(() =>
                {
                    if (includeDeleted)
                    {
                        output = table;
                    }

                    if (typeof(TEntity).GetInterface(nameof(ISoftDeletedEntity)) == null)
                    {
                        output = table;
                    }

                    output = table.OfType<ISoftDeletedEntity>().Where(entry => !entry.Deleted).OfType<TEntity>();
                    output = culture == null ? output : 
                        output.OfType<ICultureEntity>().Where(entry => entry.Culture == culture).OfType<TEntity>();
                });
            }
            
            catch (Exception ex)
            {
                //TODO log the exception
            }
            return output;
        }

        //public TEntity GetById(object id)
        //{
        //    return table.Find(id);
        //}

        public TEntity GetById(Guid guid, string culture = null)
        {
            if (culture == null)
            {
                return table.FirstOrDefault(t => t.Guid == guid);
            }

            return table.OfType<ICultureEntity>().Where(entry => entry.Culture == culture)
                .OfType<TEntity>().FirstOrDefault(t => t.Guid == guid);

        }

        public async Task<TEntity> GetByIdAsync(Guid guid, string culture = null)
        {
            TEntity output;
            if (culture == null)
            {
                output = table.FirstOrDefault(t => t.Guid == guid);
            }
            else
            {
                output = await table.OfType<ICultureEntity>().Where(entry => entry.Culture == culture)
                .OfType<TEntity>().FirstOrDefaultAsync(t => t.Guid == guid);
            }
            return output;
            //return await table.FirstOrDefaultAsync(t => t.Guid == guid);
        }

        public async Task UpdateAsync(TEntity ChangedEntity)
        {
            table.Attach(ChangedEntity);
            context.Entry(ChangedEntity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public void Update(TEntity ChangedEntity)
        {
            table.Attach(ChangedEntity);
            context.Entry(ChangedEntity).State = EntityState.Modified;
            context.SaveChanges();
            
        }
    }
}
