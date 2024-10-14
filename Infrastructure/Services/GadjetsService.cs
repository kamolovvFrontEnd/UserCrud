using AutoMapper;
using Core.Dtos.GadgetDtos;
using Core.Entity;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GadgetsService(Data _data, IMapper _mapper)
{
    public async Task<List<GetGadgetDto>> GetAllGadgets()
    {
        List<Gadget> models = await _data.Gadgets.ToListAsync();

        return _mapper.Map<List<GetGadgetDto>>(models);
    }

    public async Task<GadgetDto> AddGadget(AddGadgetDto gadget)
    {
        var item = _mapper.Map<Gadget>(gadget);

        _data.Gadgets.Add(item);

        await _data.SaveChangesAsync();

        var dto = new GadgetDto(item);
        return dto;
    }

    public async Task DeleteGadget(int id)
    {
        Gadget? item = await _data.Gadgets.FindAsync(id);
        if (item != null)
        {
            _data.Gadgets.Remove(item);
            await _data.SaveChangesAsync();
        }
    }

    public async Task<GadgetDto> UpdateGadget(UpdateGadgetDto gadget)
    {
        Gadget? item = await _data.Gadgets.FindAsync(gadget.Id);

        _mapper.Map(gadget, item!);

        await _data.SaveChangesAsync();

        var gto = _mapper.Map<GadgetDto>(item);
        return gto;
    }
}