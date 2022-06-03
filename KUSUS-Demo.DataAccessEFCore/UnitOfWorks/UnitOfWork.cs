using KUSUS_Demo.DataAccessEFCore.Repositories;
using KUSYS_DEMO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSUS_Demo.DataAccessEFCore.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Students = new StudentRepository(_context);
            Courses = new CourseRepository(_context);
            StudentCourses = new StudentCourseRepository(_context);
        }

        public IStudentRepository Students { get; private set; }
        public ICourseRepository Courses { get; private set; }

        public IStudentCourseRepository StudentCourses { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
