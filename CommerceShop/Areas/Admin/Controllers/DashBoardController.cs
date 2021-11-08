using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var req = returnUrl + "?culture=" + culture;
            //var actionName = ControllerContext.ActionDescriptor.ActionName;
            //var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            //return View("index");
            //return Redirect(req);
            //return RedirectToAction(actionName, controllerName, culture);
            return LocalRedirect(req);
        }
    }
}
