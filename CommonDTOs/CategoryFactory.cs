using CommerceShop.Areas.Admin.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs
{
    public class CategoryFactory : ICategoryFactory
    {
        /// <summary>
        /// convert ViewModel to Model
        /// </summary>
        public Category ConvertToCategory(CategoryViewModel obj)
        {
            var outCategory = new Category
            {
                Name = obj.Name,
                Description = obj.Description,
                // add slug
                MetaTitle = obj.MetaTitle,
                MetaDescription = obj.MetaDescription,
                MetaKeywords = obj.MetaKeywords,
                ParentCategoryId = obj.ParentCategoryId,
                PictureId = obj.PictureId,
                CreatedOnUtc = DateTime.UtcNow
            };
            return outCategory;
        }
    }
}
