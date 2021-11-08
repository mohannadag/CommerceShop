using Commerce.Services.Caching;
using Commerce.Core;
using Commerce.Data;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Core.Caching;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Commerce.Services
{
    /// <summary>
    /// Category service class
    /// </summary>
    public class CategoryService : ICategoryService
    {
        //private const string categoryCache = "category-cache";

        private readonly IEntityRepo<Category> categoryRepo;
        private readonly ICachingService cachingService;

        public CategoryService(IEntityRepo<Category> categoryRepo, ICachingService cachingService)
        {
            this.categoryRepo = categoryRepo;
            this.cachingService = cachingService;
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the categories
        /// </returns>
        public virtual async Task<IEnumerable<Category>> GetAllCategoriesAsync(string culture = null, bool includeDeleted = false)
        {
            //if (!cache.TryGetValue(categoryCache, out IEnumerable<Category> categories))
            //{
            //    categories = (await categoryRepo.GetAllAsync()).ToList();

            //    cache.Set(categoryCache, categories, TimeSpan.FromMinutes(5));
            //}

            IEnumerable<Category> categories;

            CacheResultModel result = await cachingService.RetreiveFromCacheAsync(CacheKeys.CategoryCache);
            if (result.cacheStatus == CacheResultModel.CacheStatus.doesNotExist)
            {
                categories = (await categoryRepo.GetAllAsync(includeDeleted)).ToList();
                result = await cachingService.SaveToCacheAsync(CacheKeys.CategoryCache, 0, 0, 30, categories, true);
            }

            categories = ((IEnumerable<Category>)result.cacheValue).Where(c => c.Culture == culture);
            return categories;
            

            
        }

        ///// <summary>
        ///// Gets all categories
        ///// </summary>
        ///// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        ///// <param name="showHidden">A value indicating whether to show hidden records</param>
        ///// <returns>
        ///// A task that represents the asynchronous operation
        ///// The task result contains the categories
        ///// </returns>
        //public virtual async Task<IList<Category>> GetAllCategoriesAsync(int storeId = 0, bool showHidden = false)
        //{
        //    var categories = await categoryRepo.GetAllAsync();
        //    return categories;
        //}

        /// <summary>
        /// Gets category by Id
        /// </summary>
        /// <param name="Id">category identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the category
        /// </returns>
        public virtual async Task<Category> GetCategoryByIdAsync(Guid guid, string culture = null)
        {
            var category = await categoryRepo.GetByIdAsync(guid, culture);
            return category;
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task AddCategoryAsync(Category category)
        {
            await categoryRepo.AddAsync(category);
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateCategoryAsync(Category category)
        {
            await categoryRepo.UpdateAsync(category);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteCategoryAsync(Category category)
        {
            //await categoryRepo.DeleteAsync(category.Id);

            //reset a "Parent category" property of all child subcategories
            var subcategories = await GetAllCategoriesByParentCategoryIdAsync(category.Guid);
            foreach (var subcategory in subcategories)
            {
                subcategory.ParentCategoryId = "0";
                await UpdateCategoryAsync(subcategory);
            }

            category.Deleted = true;
            await UpdateCategoryAsync(category);
        }

        public virtual async Task ForceDeleteCategoryAsync(Category category)
        {
            var subcategories = await GetAllCategoriesByParentCategoryIdAsync(category.Guid);
            if (subcategories == null)
            {
                await categoryRepo.DeleteEntityAsync(category);
            }
            // TODO add else statment to return message that you must delete subcategories first
        }

        public async Task<SelectList> GetListofCategories(string culture = null, bool includeDeleted = false)
        {
            var CategoriesList = new SelectList(await GetAllCategoriesAsync(culture, includeDeleted), "Guid", "Name");
            return CategoriesList;
        }

        public virtual async Task<IEnumerable<Category>> GetAllCategoriesByParentCategoryIdAsync(Guid guid, string culture = null, bool includeDeleted = false)
        {
            string nGuid = guid.ToString();
            var parCategory = (await GetAllCategoriesAsync(culture, includeDeleted)).ToList().Where(c => c.ParentCategoryId == nGuid);
            return parCategory;
        }


        #region factories
        /// <summary>
        /// convert ViewModel to Model
        /// </summary>
        //public Task<Category> ConvertToCategory()
        //{

        //}


        #endregion
    }
}
