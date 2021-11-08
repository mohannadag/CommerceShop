using Commerce.Core;
using Commerce.Core.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceShop.Areas.Admin.Models.Catalog
{
    public class CategoryViewModel : BaseEntity, ICultureEntity
    {
        #region Properties

        [Required]
        public string Name { get; set; }
        public SelectList CategoryListItem { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string MetaKeywords { get; set; }
        [Required]
        public string MetaDescription { get; set; }
        [Required]
        public string MetaTitle { get; set; }

        public string ParentCategoryId { get; set; } = null;

        public Guid? PictureId { get; set; }

        public bool ShowOnHomepage { get; set; }

        public bool IncludeInTopMenu { get; set; }

        public bool Deleted { get; set; }

        public int DisplayOrder { get; set; }

        public string Breadcrumb { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string Culture { get; set; }


        #endregion
    }
}
