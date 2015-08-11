using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DynamicJavascript.Models;
using DynamicJavascript.MVCUtilities;

namespace DynamicJavascript.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetScript(JSViewModel model)
        {
            model.AllowedModuleIds = GetAllowedModules(model.PartnerId);

            return this.JavascriptFromView(javascriptViewName: "script_js", model: model);
        }

        private List<string> GetAllowedModules(int partnerId)
        {
            switch (partnerId)
            {
                case 1: return new List<string>(new string[] { "partnerX" });
                case 2: return new List<string>(new string[] { "partnerY" });
                case 3: return new List<string>(new string[] { "partnerX", "partnerY" });
                default: break;
            }

            return new List<string>();
        }
	}
}