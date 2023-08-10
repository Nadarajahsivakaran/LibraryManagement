using Library.DataAccess.IRepository;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public StudentController(IUnitOfWork unitOfWork, ILogger<HomeController> logger) {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var students = await _unitOfWork.Student.GetAll();
                return View(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> UpSert(int studentId)
        {
            try
            {
                //create
                if(studentId ==0)
                {
                    return View(new Student());
                }

                var student = await _unitOfWork.Student.GetData(s=>s.StudentId==studentId);
                return View(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
		}

        [HttpPost]
		public async Task<IActionResult> UpSert(Student student)
		{
			try
			{
                if(ModelState.IsValid)
                {
                    //create
                    if(student.StudentId == 0)
                    {
                        await _unitOfWork.Student.Create(student);
                        TempData["success"] = "Student Created Successfully";
                    }
                    //update
                    else
					{
						await _unitOfWork.Student.Update(student);
						TempData["success"] = "Student Updated Successfully";

					}
                    return RedirectToAction(nameof(Index));
                }
                return View(student);
                
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return View(student);
			}
		}

        public async Task<IActionResult> StudentView(int studentId)
        {
            try
            {
                var student = await _unitOfWork.Student.GetData(s => s.StudentId == studentId);
                return View(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> DeleteView(int studentId)
        {
            try
            {
                var student = await _unitOfWork.Student.GetData(s => s.StudentId == studentId);
                return View(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteView(Student student)
        {
            try
            {
                await _unitOfWork.Student.Delete(student);
                TempData["success"] = "Student Deleted Successfully";
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
