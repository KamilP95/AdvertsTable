using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.Models
{
    public class DataContext : IdentityDbContext
    {
        public DataContext()
            : base("AzureConnection")
        {
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Advertisement>()
                .HasRequired(x => x.User)
                .WithMany(x => x.Advertisements)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}