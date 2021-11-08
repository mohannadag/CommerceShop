using Commerce.Core;
using Commerce.Services;
using CommerceShop.Areas.Admin.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        #region fields

        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        #endregion

        #region ctor

        public ProductController(IProductService productService, ICategoryService categoryService )
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        #endregion

        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            var model = (await productService.GetAllProductsAsync()).ToList();
            
            return View(model);
        }

        // GET: ProductController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ProductController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await categoryService.GetListofCategories();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product
                    {
                        Name = model.Name,
                        ShortDescription = model.ShortDescription,
                        FullDescription = model.FullDescription,
                        ShowOnHomepage = model.ShowOnHomepage,
                        MetaTitle = model.MetaTitle,
                        MetaDescription = model.MetaDescription,
                        MetaKeywords = model.MetaKeywords,
                        CategoryId = model.CategoryId,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                        // TODO add soft delete and the other services
                        
                        MarkAsNewStartDateTimeUtc = null,
                        MarkAsNewEndDateTimeUtc = null,
                        MarkAsNew = false,
                        Deleted = false,
                        DisplayStockQuantity = false,
                        DisplayStockAvailability = false,
                        AllowCustomerReviews = false,
                        Price = 0,
                        OldPrice = 0,
                        ProductCost = 0,
                        StockQuantity = 0
                    };
                    await productService.AddProductAsync(product);

                    //return View(nameof(Index));
                    return RedirectToAction("index");
                }
                //return RedirectToAction(nameof(Index));
                ViewBag.categories = await categoryService.GetListofCategories();
                return View();
            }
            catch
            {
                // TODO log the error
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(Guid guid)
        {
            var product = await productService.GetProductByIdAsync(guid);

            if (product == null && product.Deleted == true)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = "Product with id = " + guid + " is not found";
                return View("NotFound");
            }

            ProductViewModel model = new ProductViewModel
            {
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                ShowOnHomepage = product.ShowOnHomepage,
                MetaTitle = product.MetaTitle,
                MetaDescription = product.MetaDescription,
                MetaKeywords = product.MetaKeywords,
                CategoryId = product.CategoryId,
                CreatedOnUtc = product.CreatedOnUtc,
                UpdatedOnUtc = product.UpdatedOnUtc,
            };

            ViewBag.categories = await categoryService.GetListofCategories();

            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.categories = await categoryService.GetListofCategories();
                    return View();
                }

                var product = await productService.GetProductByIdAsync(model.Guid);

                product.Name = model.Name;
                product.ShortDescription = model.ShortDescription;
                product.FullDescription = model.FullDescription;
                product.ShowOnHomepage = model.ShowOnHomepage;
                product.MetaTitle = model.MetaTitle;
                product.MetaDescription = model.MetaDescription;
                product.MetaKeywords = model.MetaKeywords;
                product.CategoryId = model.CategoryId;
                product.CreatedOnUtc = model.CreatedOnUtc;
                product.UpdatedOnUtc = DateTime.UtcNow;
                // TODO add soft delete and the other services

                product.MarkAsNewStartDateTimeUtc = null;
                product.MarkAsNewEndDateTimeUtc = null;
                product.MarkAsNew = false;
                product.Deleted = false;
                product.DisplayStockQuantity = false;
                product.DisplayStockAvailability = false;
                product.AllowCustomerReviews = false;
                product.Price = 0;
                product.OldPrice = 0;
                product.ProductCost = 0;
                product.StockQuantity = 0;
                
                await productService.UpdateProductAsync(product);

                //return View(nameof(Index));
                return RedirectToAction("index");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(Guid guid)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid guid, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
