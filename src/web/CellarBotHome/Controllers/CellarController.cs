using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using CellarBotHome.Models;

namespace CellarBotHome.Controllers
{
    public class CellarController : Controller
    {
        //
        // GET: /Cellar/
        public ActionResult Index(int? page)
        {
            var ents = new CellarBotEntities();

            var userId = User.Identity.GetUserId();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var cellars = (from c in ents.Cellars 
                           where c.UserID == userId
                           select c)
                .OrderBy(obj => obj.Name)
                .ToList();

            if (!cellars.Any())
            {
                var defaultCellar = new Models.Cellar { Name = User.Identity.Name + "'s Cellar", UserID = userId };
                ents.Cellars.Add(defaultCellar);
                ents.SaveChanges();

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