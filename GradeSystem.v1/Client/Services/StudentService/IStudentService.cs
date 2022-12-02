

namespace GradeSystem.v1.Client.Services.StudentService
{
    public interface IStudentService
    {
        IList<Student> Students { get; set; }
        IList<Parent> Parents { get; set; }
        IList<Class> Classes { get; set; }
        Task<Student> GetStudentById(int id);
        Task<Student> GetStudentByParentId(int id);
        Task GetStudents();
        Task GetParents();
        Task GetClasses();

        Task CreateStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int id);

    }
}
