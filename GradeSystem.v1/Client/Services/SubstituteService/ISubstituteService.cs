namespace GradeSystem.v1.Client.Services.SubstituteService
{
    public interface ISubstituteService
    {
        IList<Enrollment> Substitutes { get; set; }
        IList<Enrollment> Enrollments { get; set; }
        IList<Subject> AvailableSubjects { get; set; }

        Task GetSubstitutes();
        Task GetEnrollments();

        Task DeleteSubstitute(int id);
        Task DeleteEnrollment(int id);
        Task DeleteLeave(int id,Teacher teacher);

        Task GetAvailableSubjects(DateTime startDate,DateTime endDate);
        Task CreateSubstitute(Enrollment enrollment);
        Task CreateLeave(Teacher teacher);
        Task<Enrollment> GetEnrollmentById (int id);
        Task<Enrollment> GetSubstituteById(int id);
    }
}
