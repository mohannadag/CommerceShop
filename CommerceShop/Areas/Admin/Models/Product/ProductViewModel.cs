using Commerce.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceShop.Areas.Admin.Models.Product
{
    public class ProductViewModel: BaseEntity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public bool ShowOnHomepage { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public Guid CategoryId { get; set; }
        public SelectList CategoryListItem { get; set; }

    }
}
