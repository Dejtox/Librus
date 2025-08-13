namespace GradeSystem.v1.Client.Services.EnrollmentService
{
    public interface IEnrollmentService
    {
        IList<Enrollment> Enrollments { get; set; }
        IList<Subject> Subjects { get; set; }
        IList<Class> Classes { get; set; }
        IList<Enrollment> EnrollmentsWithoutDuplicates { get; set; }

        Task GetClasses();
        Task GetSubjects();
        Task GetEnrollments();
        Task GetEnrollmentsWithoutDuplicates();
        Task<Class> GetClassByID(int id);
        Task<Subject> GetSubjectByID(int id);
        Task<Enrollment> GetEnrollmentByID(int id);

        Task UpdateEnrollment(Enrollment enrollment);
        Task DeleteEnrollment(int id);
        Task CreateEnrollment(Enrollment enrollment);
        Task CreatManyEnrollments(List<Enrollment> enrollments);
        Task<List<Enrollment>> GetEnrollmentsByClassID(int classID);
        Task<List<Enrollment>> GetEnrollmentsWithoutDuplicatesReturn();
    }
}
