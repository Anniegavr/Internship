using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class AppContext : DbContext
    {
        public DbSet <Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Grades> GradeSet { get; set; }
        public DbSet<Course> Course { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder op)
        {
            op.UseSqlServer(@"Server=localhost;Database=SchoolDB-test;Trusted_Connection=True;");
        }
    }
}
