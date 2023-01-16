namespace GradeSystem.v1.Client.Services.LessonsHoursService
{
    public interface ILessonsHoursService
    {
        IList<LessonsHours> LessonsHours { get; set; }


        Task GetLessonsHours();
        Task<LessonsHours> GetLessonsHoursByID(int id);

        Task UpdateLessonsHours(LessonsHours lessonsHours);
        Task DeleteLessonsHours(int id);
        Task CreateLessonsHours(LessonsHours lessonsHours);
    }
}
