using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KUSYS_DEMO.Domain.Entities;

namespace KUSYS_DEMO.Domain.Interfaces
{
    public interface IStudentCourseRepository:IGenericRepository<StudentCourse>
    {
        public List<StudentCourse> GetCourseOfStudentById(string studentId);
        public List<StudentCourse> GetCourseOfStudentGetAll();
    }
}
