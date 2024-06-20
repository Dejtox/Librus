
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
        public SubstituteService(HttpClient http,NavigationManager navigationManager) 
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Enrollment> Substitutes { get; set; }=new List<Enrollment>();
        public IList<Enrollment> Enrollments { get; set; }=new List<Enrollment>();
        public IList<Subject> AvailableSubjects { get; set; } =new List<Subject>();
        
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateSubstitute(Enrollment enrollment)
        {
            await _http.PutAsJsonAsync($"api/Substitute/{enrollment.EnrollmentID}", enrollment);
            _navigationManager.NavigateTo("substitutes");
        }

        public async Task GetEnrollments()
        {
            var result = await _http.GetFromJsonAsync<List<Enrollment>>("api/Enrollments");
            if (result != null)
                Enrollments = result;
        }

        public async Task DeleteEnrollment(int id)
        {
            await _http.DeleteAsync($"api/Enrollments/{id}");
        }

        public async Task GetSubstitutes()
        {
            var result = await _http.GetFromJsonAsync<List<Enrollment>>("api/Substitute");
            if (result != null)
                Substitutes = result;
        }

        public async Task GetAvailableSubjects(DateTime startDate, DateTime endDate)
        {
            //przy parametrach tak jest bo js ma format m-d-y, a c# d-m-y 
            var result = await _http.GetFromJsonAsync<List<Subject>>($"api/Substitute/available_teachers/{startDate:MM-dd-yyyy}/{endDate:MM-dd-yyyy}");
            if (result != null)
                AvailableSubjects = result;
        }

        public async Task<Enrollment> GetEnrollmentById(int id)
        {
            var result = await _http.GetFromJsonAsync<Enrollment>($"api/Enrollments/{id}");
            if (result != null)
                return result;
            throw new Exception("Enrollment not found");
        }


        public async Task DeleteSubstitute(int id)
        {
            await _http.DeleteAsync($"api/Substitute/{id}");
            _navigationManager.NavigateTo("substitutes");
        }

        public async Task<Enrollment> GetSubstituteById(int id)
        {
            var substitute = await _http.GetFromJsonAsync<Enrollment>($"api/Substitute/{id}");
            if (substitute != null)
                return substitute;
            throw new Exception("Not found");
        }

        public async Task CreateLeave(Teacher teacher)
        {
            await _http.PutAsJsonAsync($"api/Substitute/leave_add/{teacher.TeacherID}", teacher);
            _navigationManager.NavigateTo("leave");
        }

        public async Task DeleteLeave(int id,Teacher teacher)
        {
            await _http.PutAsJsonAsync($"api/Substitute/leave/{id}", teacher);
            _navigationManager.NavigateTo("leave");
        }
    }
}
