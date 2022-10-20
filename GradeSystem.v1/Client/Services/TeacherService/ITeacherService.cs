namespace GradeSystem.v1.Client.Services.TeacherService
{
    public interface ITeacherService
    {
        IList<Teacher> Teachers { get; set; }

        Task GetTeachers();
        Task<Teacher> GetTeacherByID(int id);
        Task UpdateTeacher(Teacher teacher);
        Task DeleteTeacher(int id);
        Task CreateTeacher(Teacher teacher);
    }
}
