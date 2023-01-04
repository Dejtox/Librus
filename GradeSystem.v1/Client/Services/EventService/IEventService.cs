namespace GradeSystem.v1.Client.Services.EventService
{
    public interface IEventService
    {
        IList<CalendarEvent> Events { get; set; }
        IList<Class> Classes { get; set; }

        Task GetCalendarEvents();
        Task GetClasses();

        Task<CalendarEvent> GetCalendarEventByID(int id);

        Task UpdateCalendarEvent(CalendarEvent CalendarEvent);
        Task DeleteCalendarEvent(int id);
        Task CreateCalendarEvent(CalendarEvent CalendarEvent);
    }
}
