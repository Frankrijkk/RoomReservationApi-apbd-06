using RoomReservationApi.Exceptions;
using RoomReservationApi.Models;
using RoomReservationApi.Repositories;

namespace RoomReservationApi.Services;

public class ReservationService : IReservationService
{
    public ReservationService(IReservationRepository repository,IRoomRepository roomRepository)
    {
        _reservationRepository = repository;
        _roomRepository = roomRepository;
    }
    IReservationRepository _reservationRepository;
    private IRoomRepository _roomRepository;
    public Reservation Get(long reservationId)
    {
        return _reservationRepository.GetIfExists(reservationId);
    }

    public List<Reservation> GetAll(DateOnly? dateFilter, ReservationStatus? statusFilter, long? roomIdFilter)
    {
        List<Reservation> reservations = _reservationRepository.GetAll();
        if (dateFilter != null)
        {
            reservations = reservations.Where(r=>r.Date==dateFilter).ToList();
        }

        if (statusFilter != null)
        {
            reservations = reservations.Where(r => r.Status == statusFilter).ToList();
        }

        if (roomIdFilter != null)
        {
            reservations = reservations.Where(r=>r.RoomId==roomIdFilter.Value).ToList();
        }
        return reservations;
    }

    public long AddReservation(long roomId, string organizerName, string topic, DateOnly date, TimeOnly startTime,
        TimeOnly endTime, ReservationStatus status)
    {
        Room room = _roomRepository.GetRoomIfExists(roomId);
        if (!room.IsActive)
        {
            throw new ArgumentException("Room is inactive");

        }
        
        List<Reservation> overlaps =
            _reservationRepository.GetAll().Where(r => r.RoomId == roomId && r.Date == date).ToList().Where(r=>r.StartTime<endTime&&startTime<r.EndTime).ToList();
        if (overlaps.Count > 0)
        {
            throw new ReservationConflictException("There already is a reservation conflicting with this one");
        }

        Reservation reservation = new Reservation(roomId, organizerName, topic, date, startTime, endTime, status);
        _reservationRepository.AddReservation(reservation);
        return reservation.Id;
    }

    public void UpdateReservation(long id,long? roomId, string? organizerName, string? topic, DateOnly? date, TimeOnly? startTime,
        TimeOnly? endTime, ReservationStatus? status)
    {
        if (roomId != null || date != null || startTime != null || endTime != null)
        {
            List<Reservation> overlaps =
                _reservationRepository.GetAll().Where(r => r.RoomId == roomId && r.Date == date).ToList().Where(r=>r.StartTime<endTime&&startTime<r.EndTime).ToList();
            if (overlaps.Count > 0)
            {
                throw new ReservationConflictException("There already is a reservation in conflict with the updated one");
            }
        }

        if (roomId != null)
        {
            _reservationRepository.UpdateRoomId(id,roomId.Value);
        }

        if (organizerName != null)
        {
            _reservationRepository.UpdateOrganizerName(id, organizerName);
        }

        if (topic != null)
        {
            _reservationRepository.UpdateTopic(id, topic);
        }

        if (date != null)
        {
            _reservationRepository.UpdateDate(id,date.Value);
        }

        if (startTime != null)
        {
            _reservationRepository.UpdateStartTime(id,startTime.Value);
        }

        if (endTime != null)
        {
            _reservationRepository.UpdateEndTime(id,endTime.Value);
        }

        if (status != null)
        {
            _reservationRepository.UpdateStatus(id,status.Value);
        }
        
    }

    public void Delete(long id)
    {
        _reservationRepository.DeleteReservation(id);
    }
}