using System.ComponentModel;

namespace GradeSystem.v1.Client.Services.ParentService
{
    public interface IParentService
    {
        IList<Parent> Parents { get; set; }
        IList<Student> Students { get; set; }
        Task<Parent> GetParentById(int id);
        Task<Parent> GetParentByName(string name);
        Task GetStudents();
        Task GetParents();

        Task CreateParent(Parent parent);
        Task UpdateParent(Parent parent);
        Task DeleteParent(int id);

    }
}
