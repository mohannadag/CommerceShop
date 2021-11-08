using Commerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            var model = await productService.GetAllProductsAsync();
            return View(model);
        }

        // GET: ProductController/Details/5
        public IActionResult Details(Guid id)
        {
            var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            var model = productService.GetProductByIdAsync(id, culture);
            if (model == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = "Product with id = " + id + " is not found";
                return View("NotFound");
            }
            return View(model);

            //var model = await productService.GetProductByIdAsync(id);
            //if (model == null)
            //{
            //    Response.StatusCode = 404;
            //    ViewBag.ErrorMessage = "Product with id = " + id + " is not found";
            //    return View("NotFound");
            //}
            //return View(model);
        }

        
    }
}
