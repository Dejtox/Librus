using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.StudentService
{
    public class StudentService : IStudentService
    {
        public StudentService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Student> Students { get ; set ; } = new List<Student>();
        public IList<Parent> Parents { get ; set; } = new List<Parent>();
        public IList<Class> Classes { get ; set ; } = new List<Class>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateStudent(Student student)
        {
            var result = await _http.PostAsJsonAsync("api/Students", student);
            await SetStudents(result);
        }

        public async Task DeleteStudent(int id)
        {
            var result = await _http.DeleteAsync($"api/Students/{id}");
            await SetStudents(result);
        }


        public async Task GetClasses()
        {
            var result = await _http.GetFromJsonAsync<List<Class>>("api/Classes");
            if (result != null)
                Classes = result;
        }


        public async Task GetParents()
        {
            var result = await _http.GetFromJsonAsync<List<Parent>>("api/Parents");
            if (result != null)
                Parents = result;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var result = await _http.GetFromJsonAsync<Student>($"api/Students/{id}");
            if (result != null)
                return result;
            throw new Exception("Student no find");
        }

        public async Task GetStudents()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("api/Students");
                if (result != null)
                    Students = result;
         }

        public async Task UpdateStudent(Student student)
        {
            var result = await _http.PutAsJsonAsync($"api/Students/{student.StudentID}", student);
            await SetStudents(result);
        }
        private async Task SetStudents(HttpResponseMessage result)
        {
            _navigationManager.NavigateTo("Students");
        }

        public Task<Student> GetStudentByParentId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
    

