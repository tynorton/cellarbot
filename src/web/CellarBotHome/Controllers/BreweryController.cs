using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CellarBotHome.Models;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;

namespace CellarBotHome.Controllers
{
    public class BreweryController : Controller
    {
        //
        // GET: /Brewery/
        public ActionResult Index(int? page)
        {
            var ents = new CellarBotHome.Models.CellarBotEntities();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfBreweries = ents.Breweries.OrderBy(obj => obj.name).ToPagedList(pageNumber, 25); // will only contain 25 items max because of the pageSize

            ViewBag.OnePageOfBreweries = onePageOfBreweries;
            return View();
        }

        //
        // GET: /Brewery/Details/5
        public ActionResult Details(int id)
        {
            CellarBotEntities ents = new CellarBotEntities();
            return View((from b in ents.Breweries where b.id == id select b).FirstOrDefault());
        }

        //
        // GET: /Brewery/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Brewery/Create
        [HttpPost]
        public ActionResult Create(Brewery brewery)
        {
            try
            {
                brewery.last_modified = DateTime.UtcNow;
                brewery.user_id = User.Identity.GetUserId();
                CellarBotEntities ents = new CellarBotEntities();
                ents.Breweries.Add(brewery);
                ents.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return View();
            }
        }

        //
        // GET: /Brewery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Brewery/Edit/5
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
        // GET: /Brewery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Brewery/Delete/5
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

        public JsonResult Search(string term)
        {
            var manager = new SearchManager();
            var results = manager.GetBrewerySearchResults(term);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
