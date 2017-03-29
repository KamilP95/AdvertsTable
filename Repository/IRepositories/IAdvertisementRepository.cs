using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepositories
{
    public interface IAdvertisementRepository
    {
        IQueryable<Advertisement> GetAdvertisements(bool includeCategory = false, bool includeUser = false);
    }
}
