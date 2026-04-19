using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using RoomReservationApi.Exceptions;
using RoomReservationApi.Models;
using RoomReservationApi.Services;

namespace RoomReservationApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ReservationsController : ControllerBase
{
    public class AddReservationRequest
    { 
        public long RoomId { get; set; }
        [Required] public string OrganizerName {get; set;}
        [Required]public string Topic { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public ReservationStatus Status { get; set; }
    }

    public class UpdateReservationRequest
    {
        public long? RoomId { get; set; }
        public string? OrganizerName {get; set;}
        public string? Topic { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public ReservationStatus? Status { get; set; }
    }
    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }
    IReservationService _reservationService;

    [HttpGet]
    public IActionResult GetReservations([FromQuery] DateOnly? date, [FromQuery] ReservationStatus? status,
        [FromQuery] long? roomId)
    {
        try
        {
            return Ok(_reservationService.GetAll(date,status,roomId));
        }catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{reservationId}")]
    public IActionResult GetReservation(long reservationId)
    {
        try
        {
            return Ok(_reservationService.Get(reservationId));
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

    [HttpPost]
    public IActionResult AddReservation([FromBody] AddReservationRequest rq)
    {
        try
        {
            long newId = _reservationService.AddReservation(rq.RoomId, rq.OrganizerName, rq.Topic, rq.Date,
                rq.StartTime, rq.EndTime,
                rq.Status);
            return Created($"/api/reservations/{newId}", _reservationService.GetAll(null, null, null));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (ReservationConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{reservationId}")]
    public IActionResult UpdateReservation(long reservationId, [FromBody] UpdateReservationRequest rq)
    {
        try
        {
            _reservationService.UpdateReservation(reservationId, rq.RoomId, rq.OrganizerName, rq.Topic, rq.Date,
                rq.StartTime,
                rq.EndTime, rq.Status);
            return Ok(_reservationService.GetAll(null, null, null));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e);
        }
        catch (ReservationConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500,e.Message);
        }
    }

    [HttpDelete("{reservationId}")]
    public IActionResult DeleteReservation(long reservationId)
    {
        try
        {
            _reservationService.Delete(reservationId);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return  StatusCode(500, e.Message);
        }
    }
    
}