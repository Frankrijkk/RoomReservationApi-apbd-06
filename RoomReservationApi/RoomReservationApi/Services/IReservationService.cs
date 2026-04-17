using RoomReservationApi.Models;

namespace RoomReservationApi.Services;

public interface IReservationService
{
    public List<Reservation> GetAll(DateOnly? dateFilter,ReservationStatus? statusFilter,long? roomIdFilter);
    public Reservation Get(long reservationId);
    public long AddReservation(long roomId,string organizerName,string topic,DateOnly date,TimeOnly startTime,TimeOnly endTime,ReservationStatus status);
    public void UpdateReservation(long? roomId,string? organizerName,string? topic,DateOnly? date,TimeOnly? startTime,TimeOnly? endTime,ReservationStatus? status);
    public void Delete(long id);
}