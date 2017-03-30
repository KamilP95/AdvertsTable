using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Repository.IRepositories;
using Repository.Models;

namespace Repository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDataContext _db;
        
        public CategoryRepository(IDataContext db)
        {
            _db = db;
        }

        public IQueryable<Category> GetCategories()
        {
            IQueryable<Category> categories = _db.Categories.AsNoTracking();
            categories = categories.Include(c => c.Advertisements);
            return categories;
        }
        
        public IQueryable<Advertisement> GetAdsOfCategory(int id, string sortOrder)
        {
            var advertisements =
                from ad in _db.Advertisements
                join category in _db.Categories on ad.CategoryId equals category.Id
                where category.Id == id
                select ad;

            advertisements = SortAdvertisements(advertisements, sortOrder);
            return advertisements;
        }

        private IQueryable<Advertisement> SortAdvertisements(IQueryable<Advertisement> advertisements, string sortOrder)
        {
            switch (sortOrder)
            {
                case "title":
                    advertisements = advertisements.OrderBy(ad => ad.Title);
                    break;
                case "titleDsc":
                    advertisements = advertisements.OrderByDescending(ad => ad.Title);
                    break;
                case "category":
                    advertisements = advertisements.OrderBy(ad => ad.Category.Name);
                    break;
                case "categoryDsc":
                    advertisements = advertisements.OrderByDescending(ad => ad.Category.Name);
                    break;
                case "addDate":
                    advertisements = advertisements.OrderBy(ad => ad.AddDate);
                    break;
                case "addDateDsc":
                    advertisements = advertisements.OrderByDescending(ad => ad.AddDate);
                    break;
                default:
                    advertisements = advertisements.OrderBy(ad => ad.Id);
                    break;
            }
            return advertisements;
        }
    }
}