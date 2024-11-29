namespace GradeSystem.v1.Client.Services.GradeNumberService
{
    public interface IGradeNumberService
    {
        IList<GradeNumber> GradeNumbers { get; set; }
        IList<GradeType> GradeTypes { get; set; }

        Task PostGradeNumber(GradeNumber gradenumber);
        Task PostGradeType(GradeType gradeType);
        Task PutGradeNumber(int id ,GradeNumber gradenumber);
        Task DeleteGradeNumber(int id);
        Task<GradeNumber> GetGradeNumberById(int id);
        Task GetGradeNumbers();
        Task GetGradeTypes();
    }
}
