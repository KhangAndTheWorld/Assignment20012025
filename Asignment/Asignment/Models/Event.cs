namespace Asignment.Models;

public class Event
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Status { get; set; }
}