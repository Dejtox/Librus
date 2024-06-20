using System.Text.Json.Serialization;

public class Roles
{
    public int RolesID { get; set; }
    public string Role { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public int UserID { get; set; }
}
//Roles Student, Parent, Teacher, Admin, Librarian, Principal, Secretary
//potem dodanie kontrolera roli