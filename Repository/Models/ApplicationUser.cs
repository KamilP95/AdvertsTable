using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Advertisements = new HashSet<Advertisement>();
        }

        public string Name { get; set; }
        public string LastName { get; set; }

        [DisplayName("Użytkownik:"), NotMapped]
        public string FullName => Name + " " + LastName;

        public virtual ICollection<Advertisement> Advertisements { get; private set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}