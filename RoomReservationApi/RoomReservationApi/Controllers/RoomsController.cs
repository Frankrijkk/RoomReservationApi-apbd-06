using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using RoomReservationApi.Exceptions;
using RoomReservationApi.Models;
using RoomReservationApi.Services;

namespace RoomReservationApi.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class RoomsController : ControllerBase
{
    public class CreateRoomRequest
    {
        [Required]public string Name { get; set; }
        [Required]public string BuildingCode { get; set; }
        public int Floor { get; set; }
        [Range(0,int.MaxValue)]public int Capacity { get; set; }
        public bool HasProjector { get;  set; }
        public bool IsActive { get; set; }
    }

    public class UpdateRoomRequest
    {
        public string? Name { get; set; }
        public string? BuildingCode { get; set; }
        public int? Floor { get; set; }
        [Range(0,int.MaxValue)]public int? Capacity { get; set; }
        public bool? HasProjector { get;  set; }
        public bool? IsActive { get; set; }
    }
    private IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    [HttpGet]
    public IActionResult Get([FromQueryAttribute] int? minCapacity,[FromQueryAttribute] bool? hasProjector,[FromQueryAttribute] bool? isActive)
    {
        
        return Ok(_roomService.GetAll(minCapacity, hasProjector, isActive));
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

    [HttpPost]
    public IActionResult AddRoom([FromBody] CreateRoomRequest rq)
    {
        try
        {
            long newId = _roomService.Create(rq.Name, rq.BuildingCode, rq.Floor, rq.Capacity, rq.HasProjector,
                rq.IsActive);
            return Created($"/api/rooms/{newId}", _roomService.GetAll(null, null, null));
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (ReservationConflictException e)
        {
            return StatusCode(409, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:long}")]
    public IActionResult Update( long id,[FromBody]UpdateRoomRequest rq)
    {
        try
        {
            _roomService.Update(id, rq.Name, rq.BuildingCode, rq.Floor, rq.Capacity, rq.HasProjector, rq.IsActive);
            return Ok(_roomService.GetById(id));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500,e.Message);
        }
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        try
        {
            _roomService.DeleteRoom(id);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
}