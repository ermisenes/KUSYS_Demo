using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSUS_Demo.DataAccessEFCore.ViewModels
{
    public class StudentDetailVM
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string CourseId { get; set; }
        public string NameOfCourse { get; set; }
    }
}
