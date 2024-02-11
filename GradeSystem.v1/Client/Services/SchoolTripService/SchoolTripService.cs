﻿
using GradeSystem.v1.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.SchoolTripService
{
    public class SchoolTripService : ISchoolTripService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public SchoolTripService(HttpClient http,NavigationManager navigationManager) 
        {
            _http = http;
            _navigationManager= navigationManager;
        }
        public IList<SchoolTrip> SchoolTrips { get;set; }=new List<SchoolTrip>();
        public IList<Teacher> Teachers { get;set; } =new List<Teacher>();
        public IList<Class> Classes { get; set; } = new List<Class>();

        public async Task CreateSchoolTrip(SchoolTripCombined schoolTripCombined)
        {
            await _http.PostAsJsonAsync("api/SchoolTrip", schoolTripCombined);
            _navigationManager.NavigateTo("school_trips");
        }

        public async Task GetClasses()
        {
            var result = await _http.GetFromJsonAsync<List<Class>>("api/Classes");
            if (result != null)
                Classes = result;
        }

        public async Task GetSchoolTrips()
        {
            var result = await _http.GetFromJsonAsync<List<SchoolTrip>>("api/SchoolTrip");
            if (result != null)
                SchoolTrips = result;
        }

        public async Task GetTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers");
            if (result != null)
                Teachers = result;
        }

        public async Task<SchoolTrip> GetSchoolTripByID(int id)
        {
            var result = await _http.GetFromJsonAsync<SchoolTrip>($"api/SchoolTrip/{id}");
            if (result != null)
                return result;
            throw new Exception("School trip not found");
        }

        public async Task DeleteSchoolTripByID(int id)
        {
            await _http.DeleteAsync($"api/SchoolTrip/{id}");
            _navigationManager.NavigateTo("school_trips");
        }

        public async Task UpdateSchoolTrip(SchoolTripCombined schoolTripCombined)
        {
            await _http.PutAsJsonAsync($"api/SchoolTrip/{schoolTripCombined.SchoolTrip.SchoolTripID}", schoolTripCombined);
            _navigationManager.NavigateTo("school_trips");
        }
    }
}
