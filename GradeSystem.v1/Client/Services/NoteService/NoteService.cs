
using GradeSystem.v1.Client.Services.ClassService;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.NoteService
{
    public class NoteService : INoteService
    {
        public NoteService(HttpClient http)
        {
            _http = http;
        }
        public IList<Note> Notes { get; set; } = new List<Note>();
        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        private readonly HttpClient _http;
        public async Task CreateNote(Note notes)
        {
            await _http.PostAsJsonAsync("api/Notes", notes);
        }

        public async Task DeleteNote(int id)
        {
            await _http.DeleteAsync($"api/Notes/{id}");
        }

        public async Task<Note> GetNoteByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Note>($"api/Notes/{id}");
            if (result != null)
                return result;
            throw new Exception("Note not found");
        }

        public async Task GetNotes()
        {
            var result = await _http.GetFromJsonAsync<List<Note>>("api/Notes");
            if (result != null)
                Notes = result;
        }

        public async Task UpdateNote(Note notes)
        {
            await _http.PutAsJsonAsync($"api/Notes/{notes.NoteID}", notes);
        }

        public async Task GetTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers");
            if (result != null)
                Teachers = result;
        }
    }
}
