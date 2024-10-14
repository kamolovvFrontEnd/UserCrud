using Core.Dtos.UserDtos;
using Core.Entity;

namespace Core.Dtos.GadgetDtos;

public class GetGadgetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public int UserId { get; set; }
}