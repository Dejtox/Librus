namespace GradeSystem.v1.Client.Services.ExtracurricularActivityService
{
    public interface IExtracurricularActivityService
    {
        Task<List<ExtracurricularActivity>> GetAllExtracurricularActivities();
        Task<ExtracurricularActivity> GetExtracurricularActivity(int id);
        Task<ExtracurricularActivityStudents> GetExtracurricularActivityStudent(int id);
        Task<List<ExtracurricularActivityStudents>> GetAllExtracurricularActivityStudents(int id);
        Task DeleteExtracurricularActivity(int id);
        Task CreateExtracurricularActivity(ExtracurricularActivity activity);
        Task CreateExtracurricularActivityStudent(ExtracurricularActivityStudents activityStudent);
        Task DeleteExtracurricularActivityStudent(int id,int activityid);
        Task UpdateExtracurricularActivityStudent(int id,ExtracurricularActivityStudents student);
    }
}
