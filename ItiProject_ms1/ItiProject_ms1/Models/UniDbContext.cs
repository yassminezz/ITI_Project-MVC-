using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
namespace ItiProject_ms1.Models
{
    public class UniDbContext :IdentityDbContext<IdentityUser>
    {
        public UniDbContext(DbContextOptions<UniDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudents> CourseStudents { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        //public DbSet<User>Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                 new IdentityRole
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = "Admin",
                     NormalizedName = "admin",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Hr",
                    NormalizedName = "hr",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Instructor",
                    NormalizedName = "instructor",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Student",
                    NormalizedName = "student",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                } );

        }
    }
}
