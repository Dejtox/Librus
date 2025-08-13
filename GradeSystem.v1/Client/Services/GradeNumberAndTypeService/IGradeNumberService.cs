namespace GradeSystem.v1.Client.Services.GradeNumberService
{
    public interface IGradeNumberService
    {
        IList<GradeNumber> GradeNumbers { get; set; }
        IList<GradeType> GradeTypes { get; set; }

        Task PostGradeNumber(GradeNumber gradenumber);
        Task PostGradeType(GradeType gradeType);
        Task PutGradeNumber(int id ,GradeNumber gradenumber);
        Task PutGradeType(int id, GradeType gradetype);
        Task DeleteGradeNumber(int id);
        Task DeleteGradeType(int id);
        Task<GradeNumber> GetGradeNumberById(int id);
        Task<GradeType> GetGradeTypeById(int id);
        Task GetGradeNumbers();
        Task GetGradeTypes();
    }
}
