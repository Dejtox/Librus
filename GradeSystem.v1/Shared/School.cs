public class School
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameAbbreviation { get; set; } = string.Empty;
    public int PrincipalID { get; set; }
    public Teacher? Principal { get; set; }
    public string SchoolType { get; set; } = string.Empty;
    public int SchoolNumber { get; set; }
    public int EducationYears { get; set; }
    public string Patron { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    //public string LogoUrl { get; set; } = string.Empty; poki co tak, jak bedzie wiecej szkol troche inaczej
    public bool isMain { get; set; } 
    public string Location { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int Fax { get; set; }
    public int NIP { get; set; }
    public int REGON { get; set; }
    public string Bank {get; set; } = string.Empty;
    public int OKENumber { get; set; }
    public string LearningForm { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;//publiczna niep[ubliczna
    public int BankAccountNumber { get; set; }
}