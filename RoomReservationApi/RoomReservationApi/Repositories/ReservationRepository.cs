using System.ComponentModel.DataAnnotations;
using RoomReservationApi.Models;

namespace RoomReservationApi.Repositories;

public class ReservationRepository : IReservationRepository
{
    List<Reservation> _reservations = new List<Reservation>();

    public List<Reservation> GetAll()
    {
        return _reservations;
    }

    public Reservation GetIfExists(long reservationId)
    {
        if (!_reservations.Select(r => r.Id).Contains(reservationId))
        {
            throw new ArgumentException("reservation id not found");
        }
        return  _reservations.Single(r => r.Id == reservationId);
    }

    public void AddReservation(Reservation reservation)
    {
        if (_reservations.Contains(reservation))
        {
            throw new Exception("Reservation already exists");
        }
        _reservations.Add(reservation);
    }

    public void DeleteReservation(long reservationId)
    {
        Reservation reservation = GetIfExists(reservationId);
        _reservations.Remove(reservation);

    }

    public void UpdateOrganizerName(long id, string organizerName)
    {
        Reservation reservation = GetIfExists(id);
        reservation.OrganizerName = organizerName;
    }

    public void UpdateDate(long id, DateOnly date)
    {
        Reservation reservation = GetIfExists(id);
        reservation.Date = date;
    }

    public void UpdateEndTime(long id, TimeOnly endTime)
    {
        Reservation reservation = GetIfExists(id);
        if (endTime < reservation.StartTime)
        {
            throw new ValidationException("EndTime must be greater than start time");
        }
        reservation.EndTime = endTime;
    }

    public void UpdateRoomId(long id, long roomId)
    {
        Reservation reservation = GetIfExists(id);
        reservation.RoomId = roomId;
    }

    public void UpdateTopic(long id, string topic)
    {
        Reservation reservation = GetIfExists(id);
        reservation.Topic = topic;
    }

    public void UpdateStatus(long id, ReservationStatus status)
    {
        Reservation reservation = GetIfExists(id);
        reservation.Status=status;
    }

    public void UpdateStartTime(long id, TimeOnly startTime)
    {
        Reservation reservation = GetIfExists(id);
        if (reservation.EndTime > startTime)
        {
            throw new ValidationException("EndTime must be greater than start time");
        }
        reservation.StartTime=startTime;
    }
}
