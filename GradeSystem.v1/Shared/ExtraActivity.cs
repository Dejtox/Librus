public class ExtraActivity
{
    public int ExtraActivityID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ExtracurricularActivityID { get; set; }
    public ExtracurricularActivity? ExtracurricularActivity { get; set; }
    //nw czy obecnosci jakies beda do tego, wiec wywalone jest jak jest.
}
