using Core.Dtos.UserDtos;
using Core.Entity;

namespace Core.Dtos.GadgetDtos;

public class GadgetDto
{
    public GadgetDto() { }
    
    public GadgetDto(Gadget gadget)
    {
        Id = gadget.Id;
        Title = gadget.Title;
        Model = gadget.Model;
        Price = gadget.Price;
        UserId = gadget.UserId;
    }
        
    public int Id { get; set; }
    public string Title { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public UserDto User { get; set; }
    public int UserId { get; set; }
}