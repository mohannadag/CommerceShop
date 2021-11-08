using Commerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Data
{
    public interface IEntityRepo<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity GetById(Guid guid, string culture = null);
        Task<TEntity> GetByIdAsync(Guid guid, string culture = null);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = true, string culture = null);
        void Update(TEntity ChangedEntity);
        Task UpdateAsync(TEntity ChangedEntity);
        Task DeleteAsync(Guid guid);
        Task DeleteEntityAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(Guid guid);
    }
}
