using ItiProject_ms1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItiProject_ms1.Repository
{
    public class CourseStudentRepository : BaseRepository<CourseStudents>,IBaseRepository<CourseStudents>
    {
        private readonly UniDbContext _context;

        public CourseStudentRepository(UniDbContext context): base(context)
        {
            _context = context;
        }

    
    }
}
