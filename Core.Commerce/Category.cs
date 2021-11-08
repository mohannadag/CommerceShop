using Commerce.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commerce.Core
{
    public class Category : BaseEntity, ISoftDeletedEntity, ICultureEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the parent category identifier
        /// </summary>
        public string ParentCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the parent category identifier
        /// </summary>
        //public string ParentCategoryCulture { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public Guid? PictureId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        public Category ParentCategory { get; set; }
        public List<Product> Products { get; set; }
        public string Culture { get; set; }
    }
}
