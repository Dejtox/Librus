namespace GradeSystem.v1.Client.Services.ClassService
{
    public interface IClassService
    {
        IList<Class> Classes { get; set; }
        IList<Teacher> Teachers { get; set; }


        Task GetClasses();
        Task GetTeachers();
        Task<Class> GetClassByID(int id);
        Task<Teacher> GetTeacherByID(int id);
        Task UpdateClass(Class classs);
        Task DeleteClass(int id);
        Task CreateClass(Class classs);
    }
}
