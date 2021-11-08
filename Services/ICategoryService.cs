using Commerce.Core;
using Commerce.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commerce.Services
{
    /// <summary>
    /// Category service interface
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the categories
        /// </returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync(string culture = null, bool includeDeleted = false);

        /// <summary>
        /// Gets category by Id
        /// </summary>
        /// <param name="Id">category identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the category
        /// </returns>
        Task<Category> GetCategoryByIdAsync(Guid guid, string culture = null);

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task AddCategoryAsync(Category category);

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateCategoryAsync(Category category);

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteCategoryAsync(Category category);

        Task<SelectList> GetListofCategories(string culture = null, bool includeDeleted = false);

        Task<IEnumerable<Category>> GetAllCategoriesByParentCategoryIdAsync(Guid guid, string culture = null, bool includeDeleted = false);

        Task ForceDeleteCategoryAsync(Category category);

        #region factories
        /// <summary>
        /// convert ViewModel to Model
        /// </summary>



        #endregion
    }
}