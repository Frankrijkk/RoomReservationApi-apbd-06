using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RoomReservationApi.Services;

namespace RoomReservationApi.Controllers;
[ApiController]
[Route("/api/rooms")]
public class RoomsController : ControllerBase
{
    
    private IRoomService _roomService;
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_roomService.GetAll());
    }

    [HttpGet("{id:long}")]
    public IActionResult GetById(long id)
    {
        try
        {
            return Ok(_roomService.GetById(id));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuildingCode(string buildingCode)
    {
        try
        {
            return Ok(_roomService.GetByBuildingCode(buildingCode));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    public 
    
    
}