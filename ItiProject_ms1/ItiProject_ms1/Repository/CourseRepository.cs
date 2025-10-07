using ItiProject_ms1.Models;

namespace ItiProject_ms1.Repository
{
    public class CourseRepository: BaseRepository<Course>,IBaseRepository<Course>
    {
        private UniDbContext context;
        public CourseRepository(UniDbContext context) : base(context)
        {
            this.context = context;
        }

  
    }
}
