using Commerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Services
{
    /// <summary>
    /// Product service interface
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets all Products
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the Products
        /// </returns>
        Task<IEnumerable<Product>> GetAllProductsAsync(bool includeDeleted = false, string culture = null);

        /// <summary>
        /// Gets Product by Id
        /// </summary>
        /// <param name="Id">Product identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the Product
        /// </returns>
        Task<Product> GetProductByIdAsync(Guid guid, string culture = null);

        /// <summary>
        /// Create new Product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task AddProductAsync(Product product);

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="product">Category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateProductAsync(Product product);

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteProductAsync(Product product);

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid guid, string culture = null);
    }
}
