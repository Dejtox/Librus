namespace GradeSystem.v1.Client.Services.LogRegisterService
{
    public interface ILogRegisterService
    {
        IList<Student> Students { get; set; }
        IList<Parent> Parents { get; set; }
        IList<Teacher> Teachers { get; set; }

        Task GetStudents();
        Task GetParents();
        Task GetTeachers();
        Task CreateLog(LogRegister logRegister);
    }
}
