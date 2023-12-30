public class SchoolTripCombined //ala dto
{
    public SchoolTrip SchoolTrip { get; set; }
    // relacja 1:n, na dole to samo
    public List<Teacher> Teachers { get; set; }
    public List<Class> Classes { get; set; }
}
