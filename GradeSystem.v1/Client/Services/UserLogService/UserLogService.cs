using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.UserLogService
{
    public class UserLogService
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
