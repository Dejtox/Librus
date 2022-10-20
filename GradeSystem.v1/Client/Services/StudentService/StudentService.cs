using GradeSystem.v1.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

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
        public IList<Class> Classs { get ; set ; } = new List<Class>();
        public IList<Grade> Grades { get; set; } = new List<Grade>();
        public IList<Attendance> Attendances { get ; set ; } = new List<Attendance>();
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateStudent(Student student)
        {
            var result = await _http.PostAsJsonAsync("api/Student", student);
            await SetStudents(result);
        }

        public async Task DeleteStudent(int id)
        {
            var result = await _http.DeleteAsync($"api/Student/{id}");
            await SetStudents(result);
        }

        public async Task GetAttendances()
        {
            var result = await _http.GetFromJsonAsync<List<Attendance>>("api/Attendances");
            if (result != null)
                Attendances = result;
        }

        public async Task GetClasses()
        {
            var result = await _http.GetFromJsonAsync<List<Class>>("api/Classes");
            if (result != null)
                Classs = result;
        }

        public async Task GetGrades()
        {
            var result = await _http.GetFromJsonAsync<List<Grade>>("api/Grades");
            if (result != null)
                Grades = result;
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

        public Task<Student> GetStudentByName(string name)
        {
            throw new NotImplementedException();
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
    }
}
    

