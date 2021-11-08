using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commerce.Services;
using CommerceShop.Models.Catalog;
using System.Globalization;

namespace CommerceShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var categories = await categoryService.GetAllCategoriesAsync(culture);
            var products = await productService.GetAllProductsAsync(false,culture);
            CategoryViewModel model = new CategoryViewModel()
            {
                Categories = categories,
                Products = products
            };
            return View(model);
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(Guid guid)
        {
            var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var categories = await categoryService.GetAllCategoriesAsync(culture);
            var products = await productService.GetProductsByCategoryAsync(guid,culture);
            CategoryViewModel model = new CategoryViewModel()
            {
                Categories = categories,
                Products = products
            };
            return View(model);
        }

        
    }
}
