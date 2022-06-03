using KUSYS_DEMO.Domain.Entities;
using KUSYS_DEMO.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSUS_Demo.DataAccessEFCore.Repositories
{
    public class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationContext context) : base(context)
        {

        }

        public List<StudentCourse> GetCourseOfStudentById(string studentId)
        {

            var studentOfCourse = _context.StudentCourses.Include(x => x.Course).Include(x => x.Student).Where(x => x.StudentId == studentId).ToList();
            return studentOfCourse;
        }
        public List<StudentCourse> GetCourseOfStudentGetAll()
        {
            var studentOfCourse = _context.StudentCourses.Include(x => x.Course).Include(x => x.Student).ToList();
            return studentOfCourse;
        }
    }
}
