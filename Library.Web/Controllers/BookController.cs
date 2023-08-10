using AutoMapper;
using Library.DataAccess.IRepository;
using Library.Models;
using Library.Models.DTO;
using Library.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, ILogger<HomeController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await _unitOfWork.Book.GetAll();
                return View(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        private List<SelectListItem> GetCategories()
        {

            return Enum.GetValues(typeof(BookCategory)).Cast<BookCategory>()
                        .Select(category => new SelectListItem
                        {
                            Value = ((int)category).ToString(), 
                            Text = category.ToString()
                        })
                        .ToList();
        }

        public async Task<IActionResult> UpSert(int bookId)
        {
            try
            {
                //create
                if (bookId == 0)
                {
                    BookDTO bookDTO = new()
                    {
                        CategoryList = GetCategories()
                    };

                    return View(bookDTO);
                }

                var book = await _unitOfWork.Book.GetData(b => b.BookId == bookId);
                var mappedBook = _mapper.Map<BookDTO>(book);
                mappedBook.CategoryList = GetCategories();
                return View(mappedBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpSert(BookDTO bookDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //create
                    if (bookDTO.BookId == 0)
                    {
                        var mappedBook = _mapper.Map<Book>(bookDTO);
                        await _unitOfWork.Book.Create(mappedBook);
                        TempData["success"] = "Book Created Successfully";
                    }
                    //update
                    else
                    {
                        var mappedBook = _mapper.Map<Book>(bookDTO);
                        await _unitOfWork.Book.Update(mappedBook);
                        TempData["success"] = "Book Updated Successfully";

                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(bookDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(bookDTO);
            }
        }

        public async Task<IActionResult> BookView(int bookId)
        {
            try
            {
                var book = await _unitOfWork.Book.GetData(s => s.BookId == bookId);
                return View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> DeleteView(int bookId)
        {
            try
            {
                var book = await _unitOfWork.Book.GetData(s => s.BookId == bookId);
                return View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteView(Book book)
        {
            try
            {
                await _unitOfWork.Book.Delete(book);
                TempData["success"] = "Book Deleted Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["error"] = "Something Wrong";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
