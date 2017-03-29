using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepositories
{
    public interface IDataContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<Advertisement> Advertisements { get; set; }

        int SaveChanges();
        Database Database { get; }

        DbEntityEntry Entry(object entry);
    }
}
