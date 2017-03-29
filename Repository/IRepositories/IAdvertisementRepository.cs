using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Repository.Models;

namespace Repository.IRepositories
{
    public interface IAdvertisementRepository
    {
        IQueryable<Advertisement> GetAdvertisements(bool includeCategory = false, bool includeUser = false);
        Advertisement GetAdvertisement(int id);

        void DeleteAdvertisement(int id);

        void SaveChanges();

        SelectList GetCategoriesList(int? categoryId = null);

        void AddAdvertisement(Advertisement advertisement);

        void UpdateAdvertisement(Advertisement advertisement);
    }
}
