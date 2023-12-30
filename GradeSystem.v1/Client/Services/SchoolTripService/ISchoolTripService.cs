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

        Task CreateSchoolTrip(SchoolTripCombined schoolTripCombined);
        Task<SchoolTrip> GetSchoolTripByID(int id);
        Task DeleteSchoolTripByID(int id);
        Task UpdateSchoolTrip(SchoolTripCombined schoolTripCombined);
    }
}
