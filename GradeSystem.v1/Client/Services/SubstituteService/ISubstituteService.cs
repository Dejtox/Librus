namespace GradeSystem.v1.Client.Services.SubstituteService
{
    public interface ISubstituteService
    {
        IList<Subject> Subjects { get; set; }

        Task GetSubjects();
        Task<List<Teacher>> GetTeachers();
        Task<List<TeacherDTO>> GetAbsentTeachers();
        Task DeleteTeacherSubstitute(int id);
        Task CreateTeacherSubstitute(int id, Teacher teacher);
        Task<List<Enrollment>> GetEnrollments(Teacher teacher);
        Task<Enrollment> GetEnrollment(int id);
        Task CreateSubstitute(int enrollmentID, Enrollment substitute);
    }
}
