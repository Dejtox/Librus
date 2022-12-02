namespace GradeSystem.v1.Client.Services.ExtraClassesListService
{
    public interface IExtraClassesListService
    {
        IList<ExtraClassesList> ExtraClassesLists { get; set; }
        IList<Student> Students { get; set; }
        IList<ExtraClasses> ExtraClasses  { get; set; }

        Task GetExtraClassesList();
        Task GetStudemts();
        Task GetExtraClasses();

        Task<ExtraClassesList> GetExtraClassesListByID(int id);

        Task UpdateExtraClassesList(ExtraClassesList extraClassesList);
        Task DeleteExtraClassesList(int id);
        Task CreateExtraClassesList(ExtraClassesList extraClassesList);


    }
}
