
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.LessonsHoursService
{
    public class LessonsHoursService : ILessonsHoursService
    {
        public LessonsHoursService(HttpClient http)
        {
            _http = http;
        }
        public IList<LessonsHours> LessonsHours { get; set; } = new List<LessonsHours>();
        private readonly HttpClient _http;

        public async Task CreateLessonsHours(LessonsHours lessonsHours)
        {
            await _http.PostAsJsonAsync("api/LessonsHours", lessonsHours);
        }

        public async Task DeleteLessonsHours(int id)
        {
            await _http.DeleteAsync($"api/LessonsHours/{id}");
        }

        public async Task GetLessonsHours()
        {
            var result = await _http.GetFromJsonAsync<List<LessonsHours>>("api/LessonsHours");
            if (result != null)
                await SortLessonHour(result);
        }

        public async Task<LessonsHours> GetLessonsHoursByID(int id)
        {
            var result = await _http.GetFromJsonAsync<LessonsHours>($"api/LessonsHours/{id}");
            if (result != null)
                return result;
            throw new Exception("Extra Classes not found");
            
        }

        public async Task UpdateLessonsHours(LessonsHours lessonsHours)
        {
            await _http.PutAsJsonAsync($"api/LessonsHours/{lessonsHours.ID}", lessonsHours);
        }

        public async Task SortLessonHour(List<LessonsHours> lessonHour)
        {

            for (int i = 0; i < lessonHour.Count; i = i + 1)
            {
                foreach (var segment in lessonHour)
                {
                    if (segment.LessonNo == i + 1)
                    {
                        LessonsHours.Add(segment);
                    }
                }
            }
        }
    
    }
}
