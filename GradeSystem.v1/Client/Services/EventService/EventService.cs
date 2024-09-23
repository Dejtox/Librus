
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.EventService
{
    public class EventService : IEventService
    {
        public EventService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<CalendarEvent> Events { get; set; } = new List<CalendarEvent>();
        public IList<Class> Classes { get; set; } = new List<Class>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateCalendarEvent(CalendarEvent CalendarEvent)
        {
            await _http.PostAsJsonAsync("api/Events", CalendarEvent);
            _navigationManager.NavigateTo("calendar");
        }

        public async Task DeleteCalendarEvent(int id)
        {
            await _http.DeleteAsync($"api/Events/{id}");
            _navigationManager.NavigateTo("calendar");
        }


        public async Task<CalendarEvent> GetCalendarEventByID(int id)
        {
            var result = await _http.GetFromJsonAsync<CalendarEvent>($"api/Events/{id}");
            if (result != null)
                return result;
            throw new Exception("CalendarEvent not found");
        }

        public async Task GetCalendarEvents()
        {
            var result = await _http.GetFromJsonAsync<List<CalendarEvent>>("api/Events");
            if (result != null)
                Events = result;
        }

        public async Task UpdateCalendarEvent(CalendarEvent CalendarEvent)
        {
            await _http.PutAsJsonAsync($"api/Events/{CalendarEvent.CalendarEventID}", CalendarEvent);
            _navigationManager.NavigateTo("calendar");
        }

        public async Task GetClasses()
        {
            var result = await _http.GetFromJsonAsync<List<Class>>("api/Classes");
            if (result != null)
                Classes = result;
        }
    }
}
