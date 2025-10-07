using ItiProject_ms1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItiProject_ms1.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IBaseRepository<Department>
    {
        private readonly UniDbContext _context;

        public DepartmentRepository(UniDbContext context):base(context)
        {
            _context = context;
        }

      
    }
}
