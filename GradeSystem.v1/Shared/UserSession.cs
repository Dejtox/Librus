namespace GradeSystem.v1.Shared
{
    public class UserSession
    {
        public int UserID { get; set; }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        //public string Role { get; set; }
        public List<string> Roles { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }

    }
}
