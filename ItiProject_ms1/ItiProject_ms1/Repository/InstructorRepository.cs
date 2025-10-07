using ItiProject_ms1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItiProject_ms1.Repository
{
    public class InstructorRepository : BaseRepository<Instructor>, IBaseRepository<Instructor>
    {
        private readonly UniDbContext _context;

        public InstructorRepository(UniDbContext context):base(context)
        {
            _context = context;
        }

    }
}
