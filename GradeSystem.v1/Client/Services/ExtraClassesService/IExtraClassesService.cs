namespace GradeSystem.v1.Client.Services.ExtraClassesService
{
    public interface IExtraClassesService
    {
        IList<ExtraClasses> ExtraClasses { get; set; }
        IList<Teacher> Teacher { get; set; }


        Task GetExtraClasses();
        Task GetTeachers();
        Task<ExtraClasses> GetExtraClassesByID(int id);

        Task UpdateExtraClasses(ExtraClasses extraClasses);
        Task DeleteExtraClasses(int id);
        Task CreateExtraClasses(ExtraClasses extraClasses);
    }
}
