﻿
namespace GradeSystem.v1.Server.Data
{
    public class GradeSystemv1ServerContext : DbContext
    {
        public GradeSystemv1ServerContext (DbContextOptions<GradeSystemv1ServerContext> options)
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
    }
}
