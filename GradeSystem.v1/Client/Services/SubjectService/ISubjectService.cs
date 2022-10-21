namespace GradeSystem.v1.Client.Services.SubjectService
{
    public interface ISubjectService 
    {
        IList<Subject> Subjects { get; set; }
        IList<Teacher> Teachers { get; set; }


        Task GetSubjects();
        Task GetTeachers();
        Task<Subject> GetSubjectByID(int id);
        Task<Teacher> GetTeacherByID(int id);
        Task UpdateSubject(Subject subject);
        Task DeleteSubject(int id);
        Task CreateSubject(Subject subject);
    }
}
