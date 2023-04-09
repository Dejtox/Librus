    public class BlockPlan
{
    public string Name { get; set; }
    public DateTime Starthour { get; set; }
    public DateTime Endhour { get; set; }
    public bool IsEmpty { get; set; }

    public Enrollment? Lesson { get; set; }
    public ExtraClasses? AdditionalAct { get; set; }
}