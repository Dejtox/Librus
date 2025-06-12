
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.ExamService
{
    public class ExamService : IExamService
    {
        public ExamService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public IList<Exam> Exams { get; set; } = new List<Exam>();
        public IList<Subject> Subjects { get; set; }=new List<Subject>();
        public IList<Class> Classes { get; set; } = new List<Class>();

        public async Task CreateExam(Exam exam)
        {
            await _httpClient.PostAsJsonAsync("api/Exam", exam);
        }

        public async Task DeleteExam(int id)
        {
            await _httpClient.DeleteAsync($"api/Exam/{id}");
        }

        public Task GetClasses()
        {
            throw new NotImplementedException();
        }

        public async Task<Exam> GetExamByID(int id)
        {
           return await _httpClient.GetFromJsonAsync<Exam>($"api/Exam/{id}");
        }

        public Task GetExams()
        {
            throw new NotImplementedException();
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
