using Library.Models.CustomValidation;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Library.Models.DTO
{
    public class BookDTO
    {
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
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Please enter a valid 4-digit year.")]
        public string? PublishedYear { get; set; }

        [Required]
        public BookCategory Category { get; set; }

        public IEnumerable<SelectListItem>? CategoryList { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of copies must be greater than 0.")]
        [Display(Name = "No Of Copies")]
        public int NoOfCopies { get; set; } = 0;
    }
}
