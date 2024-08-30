namespace GradeSystem.v1.Client.Services.NoteService
{
    public interface INoteService
    {
        IList<Note> Notes { get; set; }
        IList<Teacher> Teachers { get; set; }

        Task GetTeachers();
        Task GetNotes();
        Task<Note> GetNoteByID(int id);
        Task UpdateNote(Note notes);
        Task DeleteNote(int id);
        Task CreateNote(Note notes);
    }
}
