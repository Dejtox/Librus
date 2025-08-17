using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.ExamService
{
    public class ExamService : IExamService
    {
        public ExamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private readonly HttpClient _httpClient;
        public IList<Exam> Exams { get; set; } = new List<Exam>();
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
        public IList<Class> Classes { get; set; } = new List<Class>();

        public async Task CreateExam(Exam exam)
        {
            await _httpClient.PostAsJsonAsync("api/Exam", exam);
        }

        public async Task DeleteExam(int id)
        {
            await _httpClient.DeleteAsync($"api/Exam/{id}");
        }

        public async Task GetClasses()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Class>>("api/Classes");
            if (result != null)
                Classes = result;
        }

        public async Task<Exam> GetExamByID(int id)
        {
            return await _httpClient.GetFromJsonAsync<Exam>($"api/Exam/{id}");
        }

        public async Task GetExams()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Exam>>("api/Exam");
            if (result != null)
                Exams = result;
        }

        public Task GetSubjects()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateExam(Exam exam)
        {
            await _httpClient.PutAsJsonAsync($"api/Exam/{exam.ExamID}", exam);
        }
    }
}