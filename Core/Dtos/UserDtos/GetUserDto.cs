using Core.Dtos.GadgetDtos;

namespace Core.Dtos.UserDtos;

public class GetUserDto
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public List<GadgetDto> Gadget { get; set; }
}