public class User
{
    public int UserID { get; set; }
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;

}
