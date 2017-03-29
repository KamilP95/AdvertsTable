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
    }
}