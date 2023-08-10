using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("IssuingBooks")]
    public class IssuingBooks
    {
        [Key]
        public int IssuingBookId { get; set; }

        [Required, Display(Name = "Book")]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [Required, Display(Name = "Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; } = DateTime.Now;

        [Required, DataType(DataType.Date), Display(Name = "Return Date")]
        [CustomDateComparisonAttribute(nameof(IssueDate), ErrorMessage = "return Date must be greater than issue Date")]
        public DateTime ReturnDate { get; set; } = DateTime.Now;

        [Display(Name = "No Of Copies"),Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of copies must be greater than 0.")]
        public int NoOfCopies { get; set; } = 0;
    }
}
