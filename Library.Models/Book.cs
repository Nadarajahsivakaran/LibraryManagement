using Library.Models.CustomValidation;
using Library.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required, StringLength(30), Display(Name = "Book Reference")]
        public string? BookReference { get; set; }

        [Required, StringLength(30)]
        public string? ISBN { get; set; }

        [Required, StringLength(30)]
        public string? Title { get; set; }

        [Required, StringLength(30)]
        public string? Author { get; set; }

        [Required, StringLength(30)]
        public string? Publication { get; set; }

        [Required, StringLength(30)]
        public string? Edition { get; set; }

        [Required, PublishedYearRange(ErrorMessage = "Please enter a valid year.")]
        [Display(Name = "Published Year")]
        public string? PublishedYear { get; set; }

        [Required]
        public BookCategory Category { get; set; }

        [Display(Name = "No Of Copies")]
        public int NoOfCopies { get; set; } = 0;
       
    }

}