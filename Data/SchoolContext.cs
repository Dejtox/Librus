using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dziennik.Models;
using Microsoft.AspNetCore.Identity;

namespace Dziennik.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }
        public DbSet<Dziennik.Models.Student> Student { get; set; }
        public DbSet<Dziennik.Models.Class> Class { get; set; }
        public DbSet<Dziennik.Models.Enrollment> Enrollment { get; set; }
        public DbSet<Dziennik.Models.Parent> Parent { get; set; }
        public DbSet<Dziennik.Models.Subject> Subject { get; set; }
        public DbSet<Dziennik.Models.Teacher> Teacher { get; set; }

    }
}
