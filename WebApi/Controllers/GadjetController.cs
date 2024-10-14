using AutoMapper;
using Core.Dtos.GadgetDtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GadgetController(GadgetsService _gadgetsService) : Controller
{
    [HttpGet("GetAllGatgets")]
    public async Task<ActionResult<List<GetGadgetDto>>> GetAllGadgets()
    {
        return await _gadgetsService.GetAllGadgets();
    }

    [HttpPost("AddGadget")]
    public async Task<ActionResult> AddGadget(AddGadgetDto gadget)
    {
        return Ok(await _gadgetsService.AddGadget(gadget));
    }

    [HttpPut("UpdateGadget")]
    public async Task<ActionResult> UpdateGadget(UpdateGadgetDto gadget)
    {
        return Ok(await _gadgetsService.UpdateGadget(gadget));
    }

    [HttpDelete("DeleteGadget")]
    public async Task<int> DeleteGadget(int id)
    {
        await _gadgetsService.DeleteGadget(id);

        return id;
    }
}