namespace GradeSystem.v1.Client.Services.StudentService
{
    public interface IStudentService
    {
        IList<Student> Students { get; set; }
        IList<Parent> Parents { get; set; }
        IList<Class> Classs { get; set; }
        IList<Grade> Grades { get; set; }
        IList<Attendance> Attendances { get; set; }
        Task<Student> GetStudentById(int id);
        Task<Student> GetStudentByName(string name);
        Task GetStudents();
        Task GetParents();
        Task GetClasses();
        Task GetGrades();
        Task GetAttendances();

    }
}
