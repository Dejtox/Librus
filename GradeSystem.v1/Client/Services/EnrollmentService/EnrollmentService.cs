using Blazored.SessionStorage;
using GradeSystem.v1.Client.Extencion;
using GradeSystem.v1.Client.Pages;
using GradeSystem.v1.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.EnrollmentService
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ISessionStorageService _sessionStorageService;
        public EnrollmentService(HttpClient http, ISessionStorageService sessionStorageService)
        {
            _http = http;
            _sessionStorageService = sessionStorageService;

        }
        public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public IList<Enrollment> EnrollmentsWithoutDuplicates { get; set; } = new List<Enrollment>();
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
        public IList<Class> Classes { get; set; } = new List<Class>();

        private readonly HttpClient _http;

        public async Task CreateEnrollment(Enrollment enrollment)
        {
            await _http.PostAsJsonAsync("api/Enrollments", enrollment);
        }

        public async Task DeleteEnrollment(int id)
        {
            await _http.DeleteAsync($"api/Enrollments/{id}");
        }

        public async Task<Class> GetClassByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Class>($"api/Classes/{id}");
            if (result != null)
                return result;
            throw new Exception("Class not found");
        }

        public async Task GetClasses()
        {
            var result = await _http.GetFromJsonAsync<List<Class>>("api/Classes");
            if (result != null)
                Classes = result;
        }

        public async Task<Enrollment> GetEnrollmentByID(int id)
        {
            //var result = await _http.GetFromJsonAsync<Enrollment>($"api/Enrollments/{id}");          
            //if (result != null)
            //    return result;
            //throw new Exception("Enrollment not found");
            try
            {
                var response = await _http.GetAsync($"api/Enrollments/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) 
                {
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<Enrollment>();
            }
            catch (HttpRequestException ex)
            { 
                return null;
            }
        }

        public async Task GetEnrollments()
        {
            var result = await _http.GetFromJsonAsync<List<Enrollment>>("api/Enrollments");
            if (result != null)
                Enrollments = result;
        }
        public async Task GetEnrollmentsWithoutDuplicates()
        {
            var result = await _http.GetFromJsonAsync<List<Enrollment>>("api/Enrollments/without_duplicates");
            if (result != null)
                EnrollmentsWithoutDuplicates = result;
        }
        public async Task<Subject> GetSubjectByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Subject>($"api/Subjects/{id}");
            if (result != null)
                return result;
            throw new Exception("Subject not found");
        }

        public async Task GetSubjects()
        {
            var result = await _http.GetFromJsonAsync<List<Subject>>("api/Subjects");
            if (result != null)
                Subjects = result;
        }

        public async Task UpdateEnrollment(Enrollment enrollment)
        {
            await _http.PutAsJsonAsync($"api/Enrollments/{enrollment.EnrollmentID}", enrollment);
        }

        public async Task<List<Enrollment>> GetEnrollmentsByClassID(int classID)
        {
            var result= await _http.GetFromJsonAsync<List<Enrollment>>($"api/Enrollments/get_enrollments_by_classid?classID={classID}");
            if (result != null)
                return result;
            throw new Exception("Enrollment no find");
        }

        public async Task<List<Enrollment>> GetEnrollmentsWithoutDuplicatesReturn()
        {
            var result = await _http.GetFromJsonAsync<List<Enrollment>>("api/Enrollments/without_duplicates");
            if (result != null)
                return result;
            throw new Exception("Enrollment no find");
        }

        public async Task CreatManyEnrollments(List<Enrollment> enrollments)
        {
            await _http.PostAsJsonAsync("api/Enrollments/create_many", enrollments);
        }
    }
}
