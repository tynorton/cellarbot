using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CellarBotHome.Models;
using Microsoft.AspNet.Identity;

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
            var onePageOfBeers = ents.Beers
                .OrderBy(obj => obj.Brewery.name + " - " + obj.name)
                .ToPagedList(pageNumber, 25); // will only contain 25 items max because of the pageSize

            ViewBag.OnePageOfBeers = onePageOfBeers;
            return View();
        }

        //
        // GET: /Beer/Details/5
        public ActionResult Details(int id)
        {
            CellarBotEntities ents = new CellarBotEntities();
            return View((from b in ents.Beers where b.id == id select b).FirstOrDefault());
        }

        //
        // GET: /Beer/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateByUPC()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult CreateBeer(Beer beer)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    beer.last_modified = DateTime.UtcNow;
                    beer.user_id = User.Identity.GetUserId();
                    CellarBotEntities ents = new CellarBotEntities();
                    ents.Beers.Add(beer);
                    ents.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Beer/Create
        [HttpPost]
        public ActionResult CreateByUPC(Beer beer)
        {
            return CreateBeer(beer);
        }

        public ActionResult CreateBasic()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // POST: /Beer/Create
        [HttpPost]
        public ActionResult CreateBasic(Beer beer)
        {
            return CreateBeer(beer);
        }

        //
        // GET: /Beer/Edit/5
        public ActionResult Edit(int id)
        {
            CellarBotEntities ents = new CellarBotEntities();
            return View((from b in ents.Beers where b.id == id select b).FirstOrDefault());
        }

        //
        // POST: /Beer/Edit/5
        [HttpPost]
        public ActionResult Edit(Beer beer)
        {
            try
            {
                CellarBotEntities ents = new CellarBotEntities();
                var storedBeer = (from b in ents.Beers where b.id == beer.id select b).FirstOrDefault();
                storedBeer.abv = beer.abv;
                storedBeer.brewery_id = beer.brewery_id;
                storedBeer.cat_id = beer.cat_id;
                storedBeer.description = beer.description;
                storedBeer.last_modified = DateTime.UtcNow;
                storedBeer.name = beer.name;
                storedBeer.style_id = beer.style_id;
                storedBeer.upc = beer.upc;
                storedBeer.user_id = beer.user_id;

                ents.SaveChanges();

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

        public ActionResult SearchResults(string searchTerm, int? page)
        {
            var manager = new SearchManager();
            var results = manager.GetBeerSearchResults(searchTerm);
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfBeers = results.ToPagedList(pageNumber, 25);

            ViewBag.SearchTerm = searchTerm;

            return View(onePageOfBeers);
        }

        public JsonResult Search(string term)
        {
            var manager = new SearchManager();
            var results = manager.GetBeerSearchResults(term);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
