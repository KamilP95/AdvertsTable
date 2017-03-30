using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetCategories();

        IQueryable<Advertisement> GetAdsOfCategory(int id, string sortOrder);
    }
}
