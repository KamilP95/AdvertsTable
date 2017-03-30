using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
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
        
        
        public ActionResult Index(int? page, string sortOrder)
        {
            #region sortingProperties

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSort = sortOrder == "title" ? "titleDsc" : "title";
            ViewBag.CategorySort = sortOrder == "category" ? "categoryDsc" : "category";
            ViewBag.AddDateSort = sortOrder == "addDate" ? "addDateDsc" : "addDate";

            #endregion

            int currentPage = page ?? 1;
            int adsPerPage = 10;
            var advertisements = _repository.GetAdvertisements(true);
            advertisements = _repository.SortAdvertisements(advertisements, sortOrder);
            return View(advertisements.ToPagedList<Advertisement>(currentPage, adsPerPage));
        }


        public ActionResult MyAdvertisements(int? page, string sortOrder)
        {
            #region sortingProperties

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSort = sortOrder == "title" ? "titleDsc" : "title";
            ViewBag.CategorySort = sortOrder == "category" ? "categoryDsc" : "category";
            ViewBag.AddDateSort = sortOrder == "addDate" ? "addDateDsc" : "addDate";

            #endregion

            int currentPage = page ?? 1;
            int adsPerPage = 10;
            string userId = User.Identity.GetUserId();
            var advertisements = _repository.GetUserAdvertisements(userId, true);
            advertisements = _repository.SortAdvertisements(advertisements, sortOrder);
            return View(advertisements.ToPagedList<Advertisement>(currentPage, adsPerPage));
        }

        
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

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryId = _repository.GetCategoriesList();
            return View();
        }


        [HttpPost]
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
                    return RedirectToAction("MyAdvertisements");
                }
                catch (Exception e)
                {
                    return View(advertisement);
                }
            }
            ViewBag.CategoryId = _repository.GetCategoriesList(advertisement.CategoryId);
            return View(advertisement);
        }

        [Authorize]
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
            if (advertisement.UserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CategoryId = _repository.GetCategoriesList(advertisement.CategoryId);
            
            return View(advertisement);
        }

        [Authorize]
        [HttpPost]
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

        [Authorize]
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
            if (advertisement.UserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(advertisement);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken, Authorize]
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
