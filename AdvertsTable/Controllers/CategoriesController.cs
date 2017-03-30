using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;

namespace AdvertsTable.Controllers
{
    public class CategoriesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        
    }
}
