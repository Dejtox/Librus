namespace GradeSystem.v1.Client.Services.SubstituteService
{
    public interface ISubstituteService
    {
        IList<Substitute> Substitutes { get; set; }
        IList<Enrollment> Enrollments { get; set; }
        IList<Subject> AvailableSubjects { get; set; }

        Task GetSubstitutes();
        Task GetEnrollments();

        Task DeleteSubstitute(int id);
        Task DeleteEnrollment(int id);

        Task GetAvailableSubjects(DateTime startDate,DateTime endDate);
        Task CreateSubstitute(Substitute substitute);
        Task UpdateSubstitute(Substitute substitute);
        Task<Enrollment> GetEnrollmentById (int id);
        Task<Substitute> GetSubstituteById(int id);
    }
}
