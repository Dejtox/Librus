using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Roles
{
    [Required]
    public int RolesID { get; set; }
    [Required]
    public string Role { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    [Required]
    public int UserID { get; set; }
}
//Roles Student, Parent, Teacher, Admin, Librarian, Principal, Secretary
