using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Models.DTO
{
	public class IssueBookDTO
	{
		public int IssuingBookId { get; set; }

		[Required, Display(Name = "Book")]
		public int BookId { get; set; }
		public Book? Book { get; set; }
		public IEnumerable<SelectListItem>? BookList { get; set; }

		[Required, Display(Name = "Student")]
		public int StudentId { get; set; }
		public Student? Student { get; set; }
		public IEnumerable<SelectListItem>? StudentList { get; set; }

		[Required, DataType(DataType.Date), Display(Name = "Issue Date")]
		public DateTime IssueDate { get; set; } = DateTime.Now;

		[Required, DataType(DataType.Date), Display(Name = "Return Date")]
        [CustomDateComparisonAttribute(nameof(IssueDate), ErrorMessage = "return Date must be greater than issue Date")]
        public DateTime ReturnDate { get; set; } = DateTime.Now;

		[Display(Name = "No Of Copies"), Required]
		[Range(1, int.MaxValue, ErrorMessage = "Number of copies must be greater than 0.")]
		public int NoOfCopies { get; set; } = 0;
	}
}
