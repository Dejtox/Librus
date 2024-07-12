using GradeSystem.v1.Client.Pages;

namespace GradeSystem.v1.Client.Services.AttendanceService
{
    public interface IAttendanceService
    {
        IList<Attendance> Attendances { get; set; }
        IList<Enrollment> Enrollments { get; set; }
        IList<Student> Students { get; set; }


        Task GetAttendances();
        Task GetEnrollments();
        Task GetStudents();
        Task<Attendance> GetAttendanceByID(int id);
        Task<Enrollment> GetEnrollmentByID(int id);
        Task<String> GetAttendanceByStudent(int studentId, Enrollment enrollment);
        Task<Student> GetStudentByID(int id);

        Task UpdateAttendance(Attendance attendance);
        Task DeleteAttendance(int id);
        Task CreateAttendance(Attendance attendance);
    }
}
