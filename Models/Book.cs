using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using Lazar_Corina_Lab2.Migrations;

namespace Lazar_Corina_Lab2.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Display(Name ="Book Title")]
        public string Title {  get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }
        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; } // Navigation property
        // Foreign Key
        public int? AuthorID { get; set; }

        // Navigation property
        public Author? Author { get; set; }


    }
}
