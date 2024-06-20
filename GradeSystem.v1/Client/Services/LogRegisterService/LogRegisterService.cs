using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace GradeSystem.v1.Client.Services.LogRegisterService
{
    public class LogRegisterService : ILogRegisterService
    {
        private readonly HttpClient _http;
        public LogRegisterService(HttpClient http) 
        {
            _http = http;
        }
        public IList<Student> Students { get; set; }=new List<Student>();
        public IList<Parent> Parents { get; set; }=new List<Parent>();
        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();

        public async Task CreateLog(LogRegister logRegister)
        {
            await _http.PostAsJsonAsync("api/LogRegister", logRegister);
        }

        public async Task GetParents()
        {
            var result = await _http.GetFromJsonAsync<List<Parent>>("api/Parents");
            if (result != null)
                Parents = result;
        }

        public async Task GetStudents()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("api/Students");
            if (result != null)
                Students = result;
        }

        public async Task GetTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers");
            if (result != null)
                Teachers = result;
        }
    }
}
