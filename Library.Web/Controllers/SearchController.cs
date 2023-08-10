using Library.DataAccess;
using Library.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Controllers
{
	public class SearchController : Controller
	{
		private readonly ApplicationDbContext context;

		public SearchController(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task<IActionResult> Index()
		 {
			var search = Request.Query["search"];
			if (!string.IsNullOrEmpty(search))
			{
				var issuedBooks = await context.IssuingBooks
					.Include(issue => issue.Book)
					.Where(issue =>
					(issue.Book.BookReference.Contains(search) || issue.Book.Title.Contains(search) || issue.Book.Author.Contains(search)) &&
					issue.Book != null)
					.Select(issue => new IssuedBookViewModel
					{
					Book = issue.Book,
					IssuingBook = issue,
                     Student = issue.Student
                    })
					.Distinct()
				.ToListAsync();
                ViewBag.SearchValue = search;
                return View(issuedBooks);
			}
            return View(Enumerable.Empty<IssuedBookViewModel>());
        }
	}
}
