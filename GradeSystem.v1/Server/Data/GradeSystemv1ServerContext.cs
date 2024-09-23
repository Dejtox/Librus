using Microsoft.EntityFrameworkCore;
namespace GradeSystem.v1.Server.Data
{
    public class GradeSystemv1ServerContext : DbContext
    {
        
        public GradeSystemv1ServerContext(DbContextOptions<GradeSystemv1ServerContext> options)
            : base(options)
        {
        }

        
        public DbSet<Student> Student { get; set; } = default!;

        public DbSet<Parent> Parent { get; set; }

        public DbSet<Subject> Subject { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<Class> Class { get; set; }

        public DbSet<Attendance> Attendance { get; set; }

        public DbSet<Grade> Grade { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookType> BookType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ExtraClasses> ExtraClasses { get; set; }
        public DbSet<ExtraClassesList> ExtraClassesList { get; set; }
        public DbSet<CalendarEvent> CalendarEvent { get; set; }
        public DbSet<LessonsHours> LessonsHours { get; set; }
        public DbSet<SchoolTrip> SchoolTrip { get; set; }
        public DbSet<Guardians> Guardians { get; set; }
        public DbSet<SchoolTripStudents> SchoolTripStudents { get; set; }
        public DbSet<SchoolTripClasses> SchoolTripClasses { get; set; }

        public DbSet<GradeNumber> GradeNumber { get; set; }
        public DbSet<GradeType> GradeType { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Note> Note { get; set; }
    }
}
