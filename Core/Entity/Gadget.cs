namespace Core.Entity;

public class Gadget
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
}