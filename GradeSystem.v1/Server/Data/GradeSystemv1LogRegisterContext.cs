namespace GradeSystem.v1.Server.Data
{
    public class GradeSystemv1LogRegisterContext:DbContext
    {
        public GradeSystemv1LogRegisterContext(DbContextOptions<GradeSystemv1LogRegisterContext> options) : base(options) 
        {
        }

        public DbSet<LogRegister> LogRegister { get; set; }
    }
}
