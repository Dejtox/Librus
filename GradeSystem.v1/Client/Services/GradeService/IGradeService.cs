namespace GradeSystem.v1.Client.Services.GradeService
{
    public interface IGradeService
    {
        IList<Grade> Grades { get; set; }
        IList<Student> Students { get; set; }
        IList<Subject> Subjects { get; set; }


        Task GetStudents();
        Task GetSubjects();
        Task GetGrades();
        Task<Student> GetStudentByID(int id);
        Task<Subject> GetSubjectByID(int id);
        Task<Grade> GetGradeByID(int id);

        Task UpdateGrade(Grade grade);
        Task DeleteGrade(int id);
        Task CreateGrade(Grade grade);
    }
}
