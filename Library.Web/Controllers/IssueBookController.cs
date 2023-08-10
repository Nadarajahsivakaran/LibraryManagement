using AutoMapper;
using Library.DataAccess.IRepository;
using Library.Models;
using Library.Models.DTO;
using Library.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Controllers
{
    public class IssueBookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public IssueBookController(IUnitOfWork unitOfWork, ILogger<HomeController> logger,
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
                var issueBooks = await _unitOfWork.IssueBook.GetAll("Book", "Student");
                return View(issueBooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }


        public async Task<IActionResult> UpSert(int IssuingBookId)
        {
            try
            {

                var studentListItems = (await _unitOfWork.Student.GetAll())
                            .Select(s => new SelectListItem
                            {
                                Value = s.StudentId.ToString(),
                                Text = s.FirstName
                            })
                            .ToList();

                var bookListItems = (await _unitOfWork.Book.GetAll())
                        .Select(s => new SelectListItem
                        {
                            Value = s.BookId.ToString(),
                            Text = s.Title
                        })
                        .ToList();
                //create
                if (IssuingBookId == 0)
                {

                    IssueBookDTO issueBookDTO = new()
                    {
                        StudentList = studentListItems,
                        BookList = bookListItems
                    };
                    return View(issueBookDTO);
                }

                var Issuebook = await _unitOfWork.IssueBook.GetData(b => b.IssuingBookId == IssuingBookId);
                var mappedIssuebook = _mapper.Map<IssueBookDTO>(Issuebook);
                mappedIssuebook.StudentList = studentListItems;
                mappedIssuebook.BookList = bookListItems;
                return View(mappedIssuebook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpSert(IssueBookDTO issueBookDTO)
        {
            try
            {
                var studentListItems = (await _unitOfWork.Student.GetAll())
                                .Select(s => new SelectListItem
                                {
                                    Value = s.StudentId.ToString(),
                                    Text = s.FirstName
                                })
                                .ToList();

                var bookListItems = (await _unitOfWork.Book.GetAll())
                        .Select(s => new SelectListItem
                        {
                            Value = s.BookId.ToString(),
                            Text = s.Title
                        })
                        .ToList();
                if (ModelState.IsValid)
                {
                    //create
                    if (issueBookDTO.IssuingBookId == 0)
                    {
                        var isAvailable = await _unitOfWork.IssueBook.IsBookAvilable(issueBookDTO.BookId,issueBookDTO.NoOfCopies);

                        if (isAvailable)
                        {
                            var mappedIssuingBooks = _mapper.Map<IssuingBooks>(issueBookDTO);
                            await _unitOfWork.IssueBook.Create(mappedIssuingBooks);
                            TempData["success"] = "Book Issues Created Successfully";
                        }
                        else
                        {
                            ModelState.AddModelError("NoOfCopies", "Out of stock");
                            
                            issueBookDTO.StudentList = studentListItems;
                            issueBookDTO.BookList = bookListItems;
                            return View(issueBookDTO);
                        }

                        
                        
                    }
                    //update
                    else
                    {
                        var mappedIssuingBooks = _mapper.Map<IssuingBooks>(issueBookDTO);
                        await _unitOfWork.IssueBook.Update(mappedIssuingBooks);
                        TempData["success"] = "Book issue Updated Successfully";

                    }
                    return RedirectToAction(nameof(Index));
                }
                issueBookDTO.StudentList = studentListItems;
                issueBookDTO.BookList = bookListItems;
                return View(issueBookDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(issueBookDTO);
            }
        }

        public async Task<IActionResult> IssueBookView(int IssuingBookId)
        {
            try
            {
                var issueBook = await _unitOfWork.IssueBook.GetData(s => s.IssuingBookId == IssuingBookId, "Book", "Student");
                return View(issueBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> DeleteView(int IssuingBookId)
        {
            try
            {
                var issueBook = await _unitOfWork.IssueBook.GetData(s => s.IssuingBookId == IssuingBookId, "Book", "Student");
                return View(issueBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteView(IssuingBooks issuingBooks)
        {
            try
            {
                await _unitOfWork.IssueBook.Delete(issuingBooks);
                TempData["success"] = "IssueBook Deleted Successfully";
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
