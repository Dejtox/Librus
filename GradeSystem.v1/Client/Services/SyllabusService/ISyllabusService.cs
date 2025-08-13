namespace GradeSystem.v1.Client.Services.SyllabusService
{
    public interface ISyllabusService
    {
        IList<Syllabus> Syllabuses { get; set; }

        Task GetSyllabuses();
        Task<Syllabus> GetSyllabusByID(int id);
        Task UpdateSyllabus(Syllabus syllabus);
        Task DeleteSyllabus(int id);
        Task CreateSyllabus(Syllabus syllabus);
    }
}
