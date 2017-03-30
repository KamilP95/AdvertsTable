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

namespace AdvertsTable.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        // GET: Categories
        public ActionResult Index()
        {
            return PartialView(_repository.GetCategories());
        }

        public ActionResult AdsOfCategory(int id, string sortOrder)
        {
            #region sortingProperties

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSort = sortOrder == "title" ? "titleDsc" : "title";
            ViewBag.CategorySort = sortOrder == "category" ? "categoryDsc" : "category";
            ViewBag.AddDateSort = sortOrder == "addDate" ? "addDateDsc" : "addDate";

            #endregion


            var advertisements = _repository.GetAdsOfCategory(id, sortOrder);

            return View(advertisements);
        }


    }
}
