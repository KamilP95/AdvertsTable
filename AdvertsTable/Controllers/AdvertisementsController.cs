using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.IRepositories;
using Repository.Models;
using Repository.Repositories;

namespace AdvertsTable.Controllers
{
    
    public class AdvertisementsController : Controller
    {
        private readonly IAdvertisementRepository _repository;

        public AdvertisementsController(IAdvertisementRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet, Route("ogloszenia")]
        public ActionResult Index()
        {
            var advertisements = _repository.GetAdvertisements(true);
            return View(advertisements.ToList());
        }

        //[HttpGet, Route(@"ogloszenia/{id}")]
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Advertisement advertisement = db.Advertisements.Find(id);
        //    if (advertisement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(advertisement);
        //}

        //[HttpGet, Route(@"ogloszenia/dodaj")]
        //public ActionResult Create()
        //{
        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
        //    return View();
        //}

        
        //[HttpPost, Route(@"ogloszenia/dodaj")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Title,CategoryId,Contents,AddDate,UserId")] Advertisement advertisement)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Advertisements.Add(advertisement);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", advertisement.CategoryId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Email", advertisement.UserId);
        //    return View(advertisement);
        //}

        //[HttpGet, Route(@"ogloszenia/edytuj/{id}")]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Advertisement advertisement = db.Advertisements.Find(id);
        //    if (advertisement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", advertisement.CategoryId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Email", advertisement.UserId);
        //    return View(advertisement);
        //}

        //[HttpPost, Route(@"ogloszenia/edytuj/{id}")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title,CategoryId,Contents,AddDate,UserId")] Advertisement advertisement)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(advertisement).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", advertisement.CategoryId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Email", advertisement.UserId);
        //    return View(advertisement);
        //}

        //[HttpGet, Route(@"ogloszenia/usun/{id}")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Advertisement advertisement = db.Advertisements.Find(id);
        //    if (advertisement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(advertisement);
        //}

        
        //[HttpPost, Route(@"ogloszenia/usun/{id}")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Advertisement advertisement = db.Advertisements.Find(id);
        //    db.Advertisements.Remove(advertisement);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
