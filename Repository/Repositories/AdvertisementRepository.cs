using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository.IRepositories;
using Repository.Models;

namespace Repository.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly IDataContext _db;

        public AdvertisementRepository(IDataContext db)
        {
            _db = db;
        }

        public IQueryable<Advertisement> GetAdvertisements(bool includeCategory = false, bool includeUser = false)
        {
            IQueryable<Advertisement> ads = _db.Advertisements;
            if (includeCategory)
                ads = ads.Include(a => a.Category);
            if (includeUser)
                ads = ads.Include(a => a.User);

            return ads;
        }

        public Advertisement GetAdvertisement(int id)
        {
            Advertisement ad = _db.Advertisements.Find(id);
            return ad;
        }

        public void DeleteAdvertisement(int id)
        {
            Advertisement ad = GetAdvertisement(id);
            _db.Advertisements.Remove(ad);
        }

        public void SaveChanges() => _db.SaveChanges();

        public SelectList GetCategoriesList(int? categoryId)
        {
            if(categoryId.HasValue)
                return new SelectList(_db.Categories, "Id", "Name", categoryId.Value);

            return new SelectList(_db.Categories, "Id", "Name");
        }

        public void AddAdvertisement(Advertisement advertisement)
        {
            _db.Advertisements.Add(advertisement);
        }

        public void UpdateAdvertisement(Advertisement advertisement)
        {
            _db.Entry(advertisement).State = EntityState.Modified;
        }

        public IQueryable<Advertisement> SortAdvertisements(IQueryable<Advertisement> advertisements, string sortOrder)
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

        public IQueryable<Advertisement> GetUserAdvertisements(string userId, bool includeCategory = false, bool includeUser = false)
        {
            var advertisements = GetAdvertisements(includeCategory, includeUser);
            advertisements = advertisements.Where(ad => ad.UserId == userId);
            return advertisements;
        }
    }
}