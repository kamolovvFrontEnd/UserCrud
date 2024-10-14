namespace Core.Dtos.GadgetDtos;

public class UpdateGadgetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public int UserId { get; set; }
}