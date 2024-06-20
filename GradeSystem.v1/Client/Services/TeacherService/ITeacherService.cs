namespace GradeSystem.v1.Client.Services.TeacherService
{
    public interface ITeacherService
    {
        IList<Teacher> Teachers { get; set; }
        IList<Teacher> AvailableTeachers { get; set; }
        IList<Teacher> UnavailableTeachers { get; set; }

        Task GetTeachers();
        Task<Teacher> GetTeacherByID(int id);
        Task UpdateTeacher(Teacher teacher);
        Task DeleteTeacher(int id);
        Task CreateTeacher(Teacher teacher);
        Task GetAvailableTeachers();
        Task GetUnavailableTeachers();
        Task UpdateTeacherStatus();
    }
}
