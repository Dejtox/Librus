
using Microsoft.AspNetCore.Components;
using System.Drawing.Printing;
using System.Net.Http.Json;
using System.Globalization;
using System;
using GradeSystem.v1.Client.Pages;

namespace GradeSystem.v1.Client.Services.SubstituteService
{
    public class SubstituteService : ISubstituteService
    {
        public SubstituteService(HttpClient http) 
        {
            _http = http;
        }
        
        private readonly HttpClient _http;

        public IList<Subject> Subjects { get; set; } = new List<Subject>();

        public async Task<List<TeacherDTO>> GetAbsentTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<TeacherDTO>>("api/Substitute");
            if (result != null)
                return result;
            throw new Exception("Substitute not found");
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers");
            if (result != null)
                return result;
            throw new Exception("Teacher not found");
        }

        public async Task DeleteTeacherSubstitute(int id)
        {
            await _http.PutAsJsonAsync($"api/Substitute/{id}",id);
        }

        public async Task CreateTeacherSubstitute(int id, Teacher teacher)
        {
            await _http.PutAsJsonAsync($"api/Substitute/add/{id}", teacher);
        }

        public async Task<List<Enrollment>> GetEnrollments(Teacher teacher)
        {
            var query = $"api/Substitute/enrollments?id={teacher.TeacherID}&startDate={teacher.StartDate:O}&endDate={teacher.EndDate:O}";
            var result = await _http.GetFromJsonAsync<List<Enrollment>>(query);

            if (result != null)
                return result;

            throw new Exception("Enrollments not found");
        }

        public async Task<Enrollment> GetEnrollment(int id)
        {
            var result = await _http.GetFromJsonAsync<Enrollment>($"api/Enrollments/{id}");
            if (result != null)
                return result;
            throw new Exception("Enrollment not found");
        }

        public async Task CreateSubstitute(int enrollmentID, Enrollment substitute)
        {
            await _http.PostAsJsonAsync($"api/Substitute/add_substitute/{enrollmentID}", substitute);
        }

        public async Task GetSubjects()
        {
            var result = await _http.GetFromJsonAsync<List<Subject>>("api/Subjects");
            if (result != null)
                Subjects = result;
        }
    }
}
