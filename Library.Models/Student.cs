using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required, StringLength(30), Display(Name = "ID Number")]
        public string? IdNumber { get; set; }

        [Required, StringLength(30), Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required, StringLength(30), Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required,EmailAddress]
        public string? Email { get; set; }

        [Required, StringLength(100)]
        public string? Address { get; set; }

        [Required, Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
        public string? PhoneNumber { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Registered Date")]
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        [Required, DataType(DataType.Date), Display(Name = "Terminated Date")]
        [CustomDateComparisonAttribute(nameof(RegisteredDate), ErrorMessage = "Terminated Date must be greater than Registered Date")]
        public DateTime TerminatedDate { get; set; } = DateTime.Now.AddDays(1);
    }
}


