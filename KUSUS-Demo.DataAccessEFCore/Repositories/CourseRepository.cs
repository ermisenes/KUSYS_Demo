using KUSYS_DEMO.Domain.Entities;
using KUSYS_DEMO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSUS_Demo.DataAccessEFCore.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
