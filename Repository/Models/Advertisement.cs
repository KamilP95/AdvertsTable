using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Advertisement
    {
        public int Id { get; set; }

        [DisplayName("Tytuł ogłoszenia:"), MaxLength(50)]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        [DisplayName("Treść ogłoszenia:"), MaxLength(2000)]
        public string Contents { get; set; }

        [DisplayName("Data dodania:"), DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddDate { get; set; }

        public string UserId { get; set; }


        public virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }
    }
}