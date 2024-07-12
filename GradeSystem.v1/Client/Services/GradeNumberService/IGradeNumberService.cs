namespace GradeSystem.v1.Client.Services.GradeNumberService
{
    public interface IGradeNumberService
    {
        IList<GradeNumber> GradeNumbers { get; set; }

        Task PostGradeNumber(GradeNumber gradenumber);
        Task PutGradeNumber(int id ,GradeNumber gradenumber);
        Task DeleteGradeNumber(int id);
        Task<GradeNumber> GetGradeNumberById(int id);
        Task GetGradeNumbers();
    }
}
