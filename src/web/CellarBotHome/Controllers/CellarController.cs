using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using CellarBotHome.Models;
using System.Data.Entity.Validation;

namespace CellarBotHome.Controllers
{
    public class CellarController : Controller
    {
        CellarBotEntities ents = new CellarBotEntities();

        public ActionResult AddToCellar(int? id = null, int? beerId = null, string beerSearchHint = null, int? searchPage = null)
        {
            ViewBag.CellarID = id;
            ViewBag.BeerID = beerId;
            ViewBag.SearchTerm = beerSearchHint;

            // The name 'id' is used so routing works correctly, but it's really the cellar's id
            var cellarId = id;
            if (cellarId.HasValue)
            {
                // Get cellar and validate user has permission
                var cellar = (from c in ents.Cellars where c.ID == cellarId.Value select c).FirstOrDefault();

                if (beerId.HasValue)
                {
                    // Get cellar and validate user has permission
                    var beer = (from b in ents.Beers where b.id == beerId.Value select b).FirstOrDefault();
                    ViewBag.Beer = beer;

                    return View(cellar);
                }
                else if (!string.IsNullOrEmpty(beerSearchHint))
                {
                    var manager = new SearchManager();
                    var results = manager.GetBeerSearchResults(beerSearchHint);
                    var pageNumber = searchPage ?? 1; // if no page was specified in the querystring, default to the first page (1)
                    var onePageOfBeers = results.ToPagedList(pageNumber, 25);

                    ViewBag.OnePageOfBeers = onePageOfBeers;

                    return View();
                }

                if (cellar.UserID == User.Identity.GetUserId())
                {
                    return View(cellar);
                }
            }

            // Default view will require you select a cellar
            return View();
        }

        //
        // GET: /Cellar/
        public ActionResult Index(int? page)
        {
            var ents = new CellarBotEntities();

            var userId = User.Identity.GetUserId();
            var pageNumber = page ?? 1; 
            var cellars = CellarHelper.GetUserCellars(userId).ToList();

            if (User.Identity.IsAuthenticated && !cellars.Any())
            {
                var defaultCellar = new Models.Cellar { Name = User.Identity.Name + "'s Cellar", UserID = userId };
                ents.Cellars.Add(defaultCellar);

                try
                {
                    ents.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    throw;
                }

                cellars.Add(defaultCellar);
            }

            var pagedCellars = cellars.ToPagedList(pageNumber, 25);
            return View(pagedCellars);
        }

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Beer/Create
        [HttpPost]
        public ActionResult Create(Cellar cellar)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(cellar.Name))
                {
                    ModelState.AddModelError("Name", "Name is required");
                }

                try
                {
                    if (ModelState.IsValid)
                    {
                        CellarBotEntities ents = new CellarBotEntities();
                        var storedCellar = new Cellar { Name = cellar.Name, UserID = User.Identity.GetUserId() };
                        ents.Cellars.Add(storedCellar);
                        ents.SaveChanges();

                        return RedirectToAction("Details", new { id = storedCellar.ID });
                    }

                    return View(cellar);
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

        public ActionResult Details(int id)
        {
            var ents = new CellarBotEntities();

            var userId = User.Identity.GetUserId();
            var cellar = (from c in ents.Cellars
                          where c.UserID == userId
                          where c.ID == id
                          select c).FirstOrDefault();

            return View(cellar);
        }

        public ActionResult Edit(int id)
        {
            CellarBotEntities ents = new CellarBotEntities();
            var userId = User.Identity.GetUserId();
            return View((from c in ents.Cellars where c.ID == id where c.UserID == userId select c).FirstOrDefault());
        }

        //
        // POST: /Beer/Edit/5
        [HttpPost]
        public ActionResult Edit(Cellar cellar)
        {
            try
            {
                CellarBotEntities ents = new CellarBotEntities();

                var userId = User.Identity.GetUserId();
                var storedCellar = (from c in ents.Cellars where c.ID == cellar.ID where c.UserID == userId select c).FirstOrDefault();

                storedCellar.Name = cellar.Name;

                ents.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
	}
}