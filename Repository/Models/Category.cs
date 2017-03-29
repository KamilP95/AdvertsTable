using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Category
    {
        public Category()
        {
            Advertisements = new HashSet<Advertisement>();
        }
        public int Id { get; set; }
        [DisplayName("Nazwa kategorii:"), MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; private set; }
    }
}