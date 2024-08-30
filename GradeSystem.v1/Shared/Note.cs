    public class Note
    {
        public int NoteID { get; set; }
        public int TeacherID {  get; set; }
        public int? Position { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

    }