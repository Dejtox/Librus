public class BookView
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Amount { get; set; } = 0;

    public IList<int> Ids { get; set; }

}