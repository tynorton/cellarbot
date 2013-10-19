using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CellarBotHome.Controllers
{
    public class BeerController : Controller
    {
        //
        // GET: /Beer/
        public ActionResult Index(int? page)
        {
            var ents = new CellarBotHome.Models.CellarBotEntities();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfBeers = ents.Beers.ToList().ToPagedList(pageNumber, 25); // will only contain 25 products max because of the pageSize

            ViewBag.OnePageOfBeers = onePageOfBeers;
            return View();
        }

        //
        // GET: /Beer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Beer/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Beer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Beer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Beer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Beer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Beer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
