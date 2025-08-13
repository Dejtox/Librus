
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.SchoolService
{
    public class SchoolService : ISchoolService
    {
        private readonly HttpClient _httpClient;
        public SchoolService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<DayOff> DayOffs { get; set; } = new List<DayOff>();


        public async Task CreateAccessCode(AccessCode accessCode)
        {
            await _httpClient.PostAsJsonAsync("api/School/access_code", accessCode);
        }

        public async Task CreateDayOff(DayOff dayOff)
        {
            await _httpClient.PostAsJsonAsync("api/School/create_dayoff", dayOff);
        }

        public async Task CreateManyDayOffs(List<DayOff> dayOffs)
        {
            await _httpClient.PostAsJsonAsync("api/School/create_many_dayoffs", dayOffs);
        }

        public async Task CreateSchool(School school)
        {
            await _httpClient.PostAsJsonAsync("api/School", school);
        }

        public async Task DeleteAccessCode(int id)
        {
            await _httpClient.DeleteAsync($"api/School/access_code/{id}");
        }

        public async Task DeleteSchool(int id)
        {
            await _httpClient.DeleteAsync($"api/School/{id}");
        }

        public async Task<AccessCode> GetAccessCodeByID(int accessCodeID)
        {
            var result = await _httpClient.GetFromJsonAsync<AccessCode>($"api/School/access_code/{accessCodeID}");
            if (result != null)
                return result;
            throw new Exception("Access code not found");
        }

        public async Task<List<AccessCode>> GetAccessCodes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AccessCode>>("api/School/access_code");
            if (result != null)
                return result;
            throw new Exception("Access codes not found");
        }

        public async Task GetDayOffs()
        {
            var result = await _httpClient.GetFromJsonAsync<List<DayOff>>("api/School/get_dayoffs");
            if (result != null)
            {
                DayOffs = result;
            }
            else
            {
                throw new Exception("Day offs not found");
            }
        }

        public async Task<School> GetSchoolByID(int schoolID)
        {
            var result = await _httpClient.GetFromJsonAsync<School>($"api/School/{schoolID}");
            if (result != null)
                return result;
            throw new Exception("School not found");
        }

        public async Task<List<School>> GetSchools()
        {
            var result = await _httpClient.GetFromJsonAsync<List<School>>("api/School");
            if (result != null)
                return result;
            throw new Exception("Schools not found");
        }

        public async Task UpdateAccessCode(AccessCode accessCode)
        {
            await _httpClient.PutAsJsonAsync($"api/School/access_code/{accessCode.ID}", accessCode);
        }

        public async Task UpdateSchool(School school)
        {
            await _httpClient.PutAsJsonAsync($"api/School/{school.ID}", school);
        }

        public async Task<HttpResponseMessage> UseAccessCode(string accessCode)
        {
            var result=await _httpClient.PutAsync($"api/School/use_access_code/{accessCode}", null);
            return result;
        }
    }
}
