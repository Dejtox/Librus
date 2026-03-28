
using Smart.Blazor;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.ExtracurricularActivityService
{
    public class ExtracurricularActivityService : IExtracurricularActivityService
    {
        private readonly HttpClient _http;
        public ExtracurricularActivityService(HttpClient http)
        {
            _http = http;
        }
        public async Task CreateExtracurricularActivity(ExtracurricularActivity activity)
        {
            await _http.PostAsJsonAsync("api/ExtracurricularActivity", activity);
        }

        public async Task CreateExtracurricularActivityStudent(ExtracurricularActivityStudents activityStudent)
        {
            await _http.PostAsJsonAsync("api/ExtracurricularActivity/student", activityStudent);
        }

        public async Task DeleteExtracurricularActivity(int id)
        {
            await _http.DeleteAsync($"api/ExtracurricularActivity/{id}");
        }

        public async Task DeleteExtracurricularActivityStudent(int id,int activityid)
        {
            await _http.DeleteAsync($"api/ExtracurricularActivity/student/{id}/{activityid}");
        }

        public async Task<List<ExtracurricularActivity>> GetAllExtracurricularActivities()
        {
            var result = await _http.GetFromJsonAsync<List<ExtracurricularActivity>>("api/ExtracurricularActivity");
            if (result != null)
                return result;
            throw new Exception("No Extracurricular Activities found");
        }

        public async Task<List<ExtracurricularActivityStudents>> GetAllExtracurricularActivityStudents(int id)
        {
            var result = await _http.GetFromJsonAsync<List<ExtracurricularActivityStudents>>($"api/ExtracurricularActivity/students/{id}");
            if (result != null)
                return result;
            throw new Exception("Extracurricular Activity Students not found");
        }

        public Task<ExtracurricularActivity> GetExtracurricularActivity(int id)
        {
            var result = _http.GetFromJsonAsync<ExtracurricularActivity>($"api/ExtracurricularActivity/{id}");
            if (result != null)
                return result;
            throw new Exception("Extracurricular Activity not found");
        }

        public async Task<ExtracurricularActivityStudents> GetExtracurricularActivityStudent(int id)
        {
            var result= await _http.GetFromJsonAsync<ExtracurricularActivityStudents>($"api/ExtracurricularActivity/student/{id}");
            if (result != null)
                return result;
            throw new Exception("Extracurricular Activity Student not found");
        }

        public async Task UpdateExtracurricularActivityStudent(int id, ExtracurricularActivityStudents s)
        {
            await _http.PutAsJsonAsync($"api/ExtracurricularActivity/student/{id}", s);
        }
    }
}
