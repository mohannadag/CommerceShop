using Commerce.Core;
using Commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Services
{
    /// <summary>
    /// Product service class
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IEntityRepo<Product> productRepo;

        public ProductService(IEntityRepo<Product> productRepo)
        {
            this.productRepo = productRepo;
        }

        /// <summary>
        /// Gets all Products
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the Products
        /// </returns>
        public virtual async Task<IEnumerable<Product>> GetAllProductsAsync(bool includeDeleted = false, string culture = null)
        {
            var products = await productRepo.GetAllAsync(includeDeleted,culture);
            return products;
        }

        ///// <summary>
        ///// Gets all Products
        ///// </summary>
        ///// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        ///// <param name="showHidden">A value indicating whether to show hidden records</param>
        ///// <returns>
        ///// A task that represents the asynchronous operation
        ///// The task result contains the Products
        ///// </returns>
        //public virtual async Task<IList<Product>> GetAllProductsAsync(int storeId = 0, bool showHidden = false)
        //{
        //    var categories = await categoryRepo.GetAllAsync();
        //    return categories;
        //}

        /// <summary>
        /// Gets Product by Id
        /// </summary>
        /// <param name="Id">Product identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the Product
        /// </returns>
        public virtual async Task<Product> GetProductByIdAsync(Guid guid, string culture = null)
        {
            var product = await productRepo.GetByIdAsync(guid, culture);
            return product;
        }

        /// <summary>
        /// Create new Product
        /// </summary>
        /// <param name="category">Product</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task AddProductAsync(Product product)
        {
            await productRepo.AddAsync(product);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateProductAsync(Product product)
        {
            await productRepo.UpdateAsync(product);
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteProductAsync(Product product)
        {
            await productRepo.DeleteAsync(product);

            //reset a "Parent category" property of all child subcategories
            //var subcategories = await GetAllCategoriesByParentCategoryIdAsync(category.Id, true);
            //foreach (var subcategory in subcategories)
            //{
            //    subcategory.ParentCategoryId = 0;
            //    await UpdateCategoryAsync(subcategory);
            //}
        }

        public virtual async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid guid, string culture = null)
        {
            var products = (await productRepo.GetAllAsync()).ToList().Where(p => p.CategoryId == guid && p.Culture == culture);
            return products;
        }
    }
}
