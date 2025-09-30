using Microsoft.EntityFrameworkCore;

namespace ItiProject_ms1.Models
{
    public class UniDbContext:DbContext
    {
      public DbSet<Department> departments {  get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<CourseStudents> CourseStudents { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Student>students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ITI_Project;Trusted_Connection=True;Encrypt=False");
        }
    }
}
