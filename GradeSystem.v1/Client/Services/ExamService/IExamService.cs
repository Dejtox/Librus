namespace GradeSystem.v1.Client.Services.ExamService
{
public interface IExamService
{
    IList<Exam> Exams { get; set; }
    IList<Subject> Subjects { get; set; }
    IList<Class> Classes { get; set; }
    Task GetExams();
    Task GetSubjects();
    Task GetClasses();
    Task<Exam> GetExamByID(int id);
    Task UpdateExam(Exam exam);
    Task DeleteExam(int id);
    Task CreateExam(Exam exam);
}
}
