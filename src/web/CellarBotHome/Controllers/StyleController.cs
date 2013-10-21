using CellarBotHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CellarBotHome.Controllers
{
    public class StyleController : Controller
    {
        //
        // GET: /Style/
        public JsonResult Search(string term)
        {
            var manager = new SearchManager();
            var results = manager.GetStyleSearchResults(term);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
	}
}