namespace GradeSystem.v1.Client.Services.SchoolTripService
{
    public interface ISchoolTripService
    {
        IList<SchoolTrip> SchoolTrips { get; set; }
        IList<Teacher> Teachers { get; set; }
        IList<Class> Classes { get; set; }

        Task GetClasses();
        Task GetTeachers();
        Task GetSchoolTrips();

        Task<List<SchoolTrip>> GetSchoolTripss();
        Task CreateSchoolTrip(SchoolTrip schoolTrip);
        Task<SchoolTrip> GetSchoolTripByID(int id);
        Task DeleteSchoolTripByID(int id);
        Task UpdateSchoolTrip(SchoolTrip schoolTrip);
        Task<List<Student>> GetStudents(List<int> classes);
    }
}
