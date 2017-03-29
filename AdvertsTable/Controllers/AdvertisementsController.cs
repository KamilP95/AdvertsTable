using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        [HttpGet, Route(@"ogloszenia/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repository.GetAdvertisement(id.Value);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        [HttpGet, Route(@"ogloszenia/dodaj")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = _repository.GetCategoriesList();
            return View();
        }


        [HttpPost, Route(@"ogloszenia/dodaj")]
        [ValidateAntiForgeryToken, Authorize]
        public ActionResult Create([Bind(Include = "Title,CategoryId,Contents")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                advertisement.UserId = User.Identity.GetUserId();
                advertisement.AddDate = DateTime.Now;
                try
                {
                    _repository.AddAdvertisement(advertisement);
                    _repository.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return View(advertisement);
                }
            }
            ViewBag.CategoryId = _repository.GetCategoriesList(advertisement.CategoryId);
            return View(advertisement);
        }

        [HttpGet, Route(@"ogloszenia/edytuj/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repository.GetAdvertisement(id.Value);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = _repository.GetCategoriesList(advertisement.CategoryId);
            
            return View(advertisement);
        }

        [HttpPost, Route(@"ogloszenia/edytuj/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,CategoryId,Contents,AddDate,UserId")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateAdvertisement(advertisement);
                    _repository.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.Error = true;
                    return View(advertisement);
                }
                
            }
            ViewBag.CategoryId = _repository.GetCategoriesList(advertisement.CategoryId);
            ViewBag.Error = false;
            return View(advertisement);
        }

        [HttpGet, Route(@"ogloszenia/usun/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repository.GetAdvertisement(id.Value);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            

            return View(advertisement);
        }


        [HttpPost, Route(@"ogloszenia/usun/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteAdvertisement(id);
            try
            {
                _repository.SaveChanges();
            }
            catch (Exception e)
            {
                return RedirectToAction("Delete", new {id = id});
            }
            return RedirectToAction("Index");
        }
    }
}
