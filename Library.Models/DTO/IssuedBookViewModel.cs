using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTO
{
	public class IssuedBookViewModel
	{
		public Book? Book { get; set; }
		public IssuingBooks? IssuingBook { get; set; }
        public Student? Student { get; set; }
    }
}
