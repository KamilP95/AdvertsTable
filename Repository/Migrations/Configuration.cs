using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Models;

namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repository.Models.DataContext context)
        {
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();
            
            SeedUsers(context);
            SeedCategories(context);
            SeedAdvertisements(context);
        }
        
        private void SeedUsers(DataContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            
            var user = new ApplicationUser() { UserName = "admin@example.com", Email = "admin@example.com" };
            manager.Create(user, "admin1");
            
            
        }
        private void SeedCategories(DataContext context)
        {
            for (int i = 1; i <= 10; i++)
            {
                var category = new Category() { Id = i, Name = "Kategoria " + i };
                context.Set<Category>().AddOrUpdate(category);
            }
            context.SaveChanges();
        }

        private void SeedAdvertisements(DataContext context)
        {
            var useerId = context.Set<ApplicationUser>().FirstOrDefault()?.Id;
            for (int i = 0; i < 10; i++)
            {
                var ad = new Advertisement()
                {
                    Id = i + 1,
                    UserId = useerId,
                    Title = "Tytul ogloszenia " + (i + 1),
                    CategoryId = (i + 2) / 2,
                    Contents = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    AddDate = DateTime.Now.AddDays(-i)
                };
                context.Set<Advertisement>().AddOrUpdate(ad);
            }
            context.SaveChanges();
        }
    }
}

