using Commerce.Services;
using CommerceShop.Areas.Admin.Models;
using Commerce.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceShop.Areas.Admin.Models.Catalog;
using Commerce.Services.Caching;
using Commerce.Core.Caching;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Commerce.Core.Settings;

namespace CommerceShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region fields
        private readonly IOptions<RequestLocalizationOptions> _locOptions;
        private readonly ICategoryService categoryService;
        private readonly ICachingService cachingService;

        #endregion

        #region ctor

        public CategoryController(ICategoryService categoryService, ICachingService cachingService)
        {
            this.categoryService = categoryService;
            this.cachingService = cachingService;
        }

        #endregion

        #region Utilities

        //public async Task<List<CategoryListItemModel>> GetCategoryByParent()
        //{
        //    List<CategoryListItemModel> items = new List<CategoryListItemModel>();

        //    List<Category> allCategories = (List<Category>)await categoryService.GetAllCategoriesAsync();

        //    List<Category> parentCategories = (List<Category>)allCategories.Where(c => c.ParentCategoryId == 0).OrderBy(c => c.Name);

        //    foreach (var cat in parentCategories)
        //    {
        //        items.Add(new CategoryListItemModel { Value = cat.Id, Text = cat.Name });
        //        GetSubTree(allCategories, cat, items);
        //    }
        //    return items;
        //}

        //private void GetSubTree(List<Category> allCategories, Category parent, List<CategoryListItemModel> items)
        //{
        //    var subCats = allCategories.Where(c => c.ParentCategoryId == parent.Id);

        //    foreach (var cat in subCats)
        //    {
        //        items.Add(new CategoryListItemModel { Value = cat.Id, Text = parent.Name + ">>" + cat.Name });
        //        GetSubTree(allCategories, cat, items);
        //    }
        //}

        private async Task GetSubTree(List<Category> allCategories, Category parent, List<Category> items)
        {
            var subCats = await categoryService.GetAllCategoriesByParentCategoryIdAsync(parent.Guid, "en");

            foreach (var cat in subCats)
            {
                //items.Add(new CategoryListItemModel { Value = cat.Id, Text = parent.Name + ">>" + cat.Name });
                cat.Name = parent.Name + " >> " + cat.Name;
                items.Add(cat);
                await GetSubTree(allCategories, cat, items);
            }
        }

        #endregion
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            var categories = (await categoryService.GetAllCategoriesAsync("en")).ToList();

            CacheResultModel result = await cachingService.RetreiveFromCacheAsync(CacheKeys.CategoryListCache);
            if (result.cacheStatus == CacheResultModel.CacheStatus.doesNotExist)
            {
                List<Category> items = new List<Category>();
                var parentCategories = categories.Where(c => c.ParentCategoryId == null).OrderBy(c => c.Name);
                foreach (var cat in parentCategories)
                {
                    items.Add(cat);
                    await GetSubTree(categories, cat, items);
                }
                result = await cachingService.SaveToCacheAsync(CacheKeys.CategoryListCache, 0, 1, 30, categories, true);
            }


            return View(result.cacheValue);
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(Guid guid)
        {
            var model = await categoryService.GetCategoryByIdAsync(guid);
            return View(model);
        }

        // GET: CategoryController/Create
        public async Task<IActionResult> Create()
        {
            CategoryViewModel model = new CategoryViewModel
            {
                CategoryListItem = await categoryService.GetListofCategories(Language.DefultLanguage),
                Culture = Language.DefultLanguage
            };

            return View(model);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = new Category
                    {
                        Guid = Guid.NewGuid(),
                        Culture = Language.DefultLanguage, // it must be the defult culture
                        Name = model.Name,
                        Description = model.Description,
                        // add slug
                        MetaTitle = model.MetaTitle,
                        MetaDescription = model.MetaDescription,
                        MetaKeywords = model.MetaKeywords,
                        ParentCategoryId = model.ParentCategoryId,
                        PictureId = model.PictureId,
                        CreatedOnUtc = DateTime.UtcNow
                    };

                    await categoryService.AddCategoryAsync(category);

                    return RedirectToAction(nameof(Index));
                }
                // maybe i can add hidden input in the view and pass it to the action
                model.CategoryListItem = await categoryService.GetListofCategories();
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(Guid id, string culture = "en")
        {
            var category = await categoryService.GetCategoryByIdAsync(id, culture);
            var basecat = await categoryService.GetCategoryByIdAsync(id, Language.DefultLanguage);

            // cheack if the category is exists or not
            if(basecat == null)
            {
                return NotFound(id);
            }

            // cheack if the category with the specific culture not exists
            if (category == null)
            {
                CategoryViewModel model = new CategoryViewModel
                {
                    CategoryListItem = await categoryService.GetListofCategories(culture),
                    Culture = culture,
                    Guid = id
                };
                return View(model);   
            }
            else
            {
                CategoryViewModel model = new CategoryViewModel
                {
                    Guid = id,
                    Culture = category.Culture,
                    Name = category.Name,
                    Description = category.Description,
                    // add slug
                    MetaTitle = category.MetaTitle,
                    MetaDescription = category.MetaDescription,
                    MetaKeywords = category.MetaKeywords,
                    ParentCategoryId = category.ParentCategoryId,
                    PictureId = category.PictureId,
                    CreatedOnUtc = category.CreatedOnUtc,
                    UpdatedOnUtc = category.UpdatedOnUtc,
                    CategoryListItem = await categoryService.GetListofCategories(culture)
                };

                return View(model);
            }
            
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                
                var baseCategory = await categoryService.GetCategoryByIdAsync(model.Guid, Language.DefultLanguage);

                if(baseCategory == null)
                {
                    throw new ArgumentNullException(nameof(model.Guid));
                }

                var category = await categoryService.GetCategoryByIdAsync(model.Guid, model.Culture);

                if (category == null)
                {
                    Category newCategory = new Category();
                    newCategory.Guid = model.Guid;
                    newCategory.Culture = model.Culture;
                    newCategory.Name = model.Name;
                    newCategory.Description = model.Description;
                    newCategory.MetaDescription = model.MetaDescription;
                    newCategory.MetaKeywords = model.MetaKeywords;
                    newCategory.MetaTitle = model.MetaTitle;
                    newCategory.ParentCategoryId = model.ParentCategoryId;
                    newCategory.PictureId = model.PictureId; // I have to process the pic then save it
                    newCategory.UpdatedOnUtc = DateTime.UtcNow;
                    newCategory.Deleted = model.Deleted;

                    await categoryService.AddCategoryAsync(newCategory);

                    baseCategory.ParentCategoryId = model.ParentCategoryId;
                    await categoryService.UpdateCategoryAsync(baseCategory);
                }
                else
                {
                    category.Guid = model.Guid;
                    category.Culture = model.Culture;
                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.MetaDescription = model.MetaDescription;
                    category.MetaKeywords = model.MetaKeywords;
                    category.MetaTitle = model.MetaTitle;
                    category.ParentCategoryId = model.ParentCategoryId;
                    category.PictureId = model.PictureId; // I have to process the pic then save it
                    category.UpdatedOnUtc = DateTime.UtcNow;
                    category.Deleted = model.Deleted;

                    await categoryService.UpdateCategoryAsync(category);

                    baseCategory.ParentCategoryId = model.ParentCategoryId;
                    await categoryService.UpdateCategoryAsync(baseCategory);
                }
                    

                // pass the category to the view

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // TODO log the exeption
                return View(model);
            }
        }

        // GET: CategoryController/Delete/5
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    // TODO make two methods in the service to (soft - force) deleting
        //    return View();
        //}

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                var category = await categoryService.GetCategoryByIdAsync(guid);

                if (category == null)
                {
                    Response.StatusCode = 404;
                    ViewBag.ErrorMessage = "Product with id = " + guid + " is not found";
                    return View("NotFound");
                }

                await categoryService.DeleteCategoryAsync(category);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
