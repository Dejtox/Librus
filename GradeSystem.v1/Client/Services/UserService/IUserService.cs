namespace GradeSystem.v1.Client.Services.UserService
{
    public interface IUserService
    {
        IList<User> Users { get; set; }
        IList<Teacher> Teachers { get; set; }
        IList<Student> Students { get; set; }
        IList<Parent> Parents { get; set; }

        Task GetTeachers();
        Task GetUsers();
        Task GetStudents();
        Task GetParents();
        Task<User> GetUserByID(int id);
        Task<User> GetUserByLogin(string login);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task CreateUser(UserDto user);
    }
}
