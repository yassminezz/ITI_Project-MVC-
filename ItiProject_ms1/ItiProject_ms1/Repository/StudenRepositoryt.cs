using ItiProject_ms1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItiProject_ms1.Repository
{
    public class StudenRepositoryt :BaseRepository<Student> ,IBaseRepository<Student>
    {
        private UniDbContext context;
        public StudenRepositoryt(UniDbContext context):base(context) 
        {
            this.context = context;
        }
      
    }
}
