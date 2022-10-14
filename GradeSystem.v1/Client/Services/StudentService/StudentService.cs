using GradeSystem.v1.Client.Pages;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.StudentService
{
    public class StudentService : IStudentService
    {
        public StudentService(HttpClient http)
        {
            _http = http;
        }
        public IList<Student> Students { get ; set ; } = new List<Student>();
        public IList<Parent> Parents { get ; set; } = new List<Parent>();
        public IList<Class> Classs { get ; set ; } = new List<Class>();
        public IList<Grade> Grades { get; set; } = new List<Grade>();
        public IList<Attendance> Attendances { get ; set ; } = new List<Attendance>();
        public HttpClient _http { get; }

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
    }
}
    

