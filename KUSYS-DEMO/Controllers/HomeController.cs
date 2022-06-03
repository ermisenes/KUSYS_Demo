using System.Diagnostics;
using KUSUS_Demo.DataAccessEFCore.ViewModels;
using KUSYS_DEMO.Domain.Entities;
using KUSYS_DEMO.Domain.Interfaces;
using KUSYS_DEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYS_DEMO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var student = _unitOfWork.Students.GetAll();

            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var student = new Student()
            {
                StudentId = model.StudentId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
            };
            try
            {
                var studentCheckDb = _unitOfWork.Students.Find(x => x.StudentId == student.StudentId).FirstOrDefault();
                if (studentCheckDb != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                _unitOfWork.Students.Add(student);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var student = _unitOfWork.Students.Find(x => x.StudentId == id).FirstOrDefault();
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _unitOfWork.Students.Remove(student);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            var student = _unitOfWork.StudentCourses.GetCourseOfStudentById(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }
            List<StudentDetailVM> result = new List<StudentDetailVM>();
            foreach (var item in student)
            {
                var model = new StudentDetailVM()
                {
                    StudentId = item.StudentId,
                    BirthDate = item.Student.BirthDate,
                    FirstName = item.Student.FirstName,
                    LastName = item.Student.LastName,
                    NameOfCourse = item.Course.CourseName,
                    CourseId = item.Course.CourseId
                };
                result.Add(model);
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult DeleteCourseOfStudent(string courseId, string studentId)
        {
            var student = _unitOfWork.StudentCourses.Find(x => x.StudentId == studentId && x.CourseId == courseId).FirstOrDefault();
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _unitOfWork.StudentCourses.Remove(student);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(string? id)
        {
            var data = _unitOfWork.Students.Find(x => x.StudentId == id).FirstOrDefault();
            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new StudentVM()
            {
                StudentId = data.StudentId,
                BirthDate = data.BirthDate,
                FirstName = data.FirstName,
                LastName = data.LastName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(StudentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var student = _unitOfWork.Students.Find(x => x.StudentId == model.StudentId).FirstOrDefault();
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            student.StudentId = model.StudentId;
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.BirthDate = model.BirthDate;
            try
            {
                _unitOfWork.Complete();
                TempData["Message"] = $"{student.StudentId} numaralı öğrenci başarılı bir şekilde güncellendi";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddCourse(string? id)
        {
            var student = _unitOfWork.Students.Find(x => x.StudentId == id).FirstOrDefault();
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new CourseVM()
            {
                StudentId = student.StudentId
            };

            ViewBag.CourseList = GetCourseList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Addcourse(CourseVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var course = _unitOfWork.StudentCourses.Find(x => x.StudentId == model.StudentId).ToList();

            foreach (var item in course)
            {
                if (item.CourseId == model.CourseId)
                {
                    ViewBag.CourseList = GetCourseList();
                    TempData["Message"] = "Bu Ders Daha önce Eklenmiştir";
                    return View(model);
                }
            }
            var Student = new StudentCourse()
            {
                CourseId = model.CourseId,
                StudentId = model.StudentId
            };

            try
            {
                _unitOfWork.StudentCourses.Add(Student);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
        private List<SelectListItem> GetCourseList()
        {
            var courses = _unitOfWork.Courses.GetAll().OrderBy(x => x.CourseName).ToList();

            var courseList = new List<SelectListItem>()
            {
                new SelectListItem("Kategori Seçiniz",null)
            };
            foreach (var item in courses)
            {
                courseList.Add(new SelectListItem(item.CourseName, item.CourseId));
            }

            return courseList;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}