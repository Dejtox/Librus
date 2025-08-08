namespace GradeSystem.v1.Client.Services.SchoolService
{
    public interface ISchoolService
    {
        Task<School> GetSchoolByID(int schoolID);
        Task<List<School>> GetSchools();
        Task CreateSchool(School school);
        Task UpdateSchool(School school);
        Task DeleteSchool(int id);
        Task<AccessCode> GetAccessCodeByID(int accessCodeID);
        Task<List<AccessCode>> GetAccessCodes();
        Task CreateAccessCode(AccessCode accessCode);
        Task UpdateAccessCode(AccessCode accessCode);
        Task DeleteAccessCode(int id);
        Task<HttpResponseMessage> UseAccessCode(string accessCode);
    }
}
