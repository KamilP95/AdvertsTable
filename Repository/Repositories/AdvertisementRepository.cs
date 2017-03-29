using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Repository.IRepositories;
using Repository.Models;

namespace Repository.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly DataContext _db = new DataContext();

        public IQueryable<Advertisement> GetAdvertisements(bool includeCategory = false, bool includeUser = false)
        {
            IQueryable<Advertisement> ads = _db.Advertisements;
            if (includeCategory)
                ads = ads.Include(a => a.Category);
            if (includeUser)
                ads = ads.Include(a => a.User);

            return ads;
        }

    }
}