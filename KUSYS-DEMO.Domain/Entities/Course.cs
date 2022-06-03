using System.ComponentModel.DataAnnotations;

namespace KUSYS_DEMO.Domain.Entities
{
    public class Course
    {
        [Key]
        public string CourseId { get; set; }
        public string CourseName { get; set; }

        public virtual List<StudentCourse> StudentCourses { get; set; }
    }
}
