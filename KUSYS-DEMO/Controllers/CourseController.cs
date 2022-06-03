using KUSYS_DEMO.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_DEMO.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var result = _unitOfWork.StudentCourses.GetCourseOfStudentGetAll();
            return View(result);
        }
        public IActionResult GetCourse()
        {
            var data = _unitOfWork.Courses.GetAll().ToList();  
            return View(data);
        }

    }
}
