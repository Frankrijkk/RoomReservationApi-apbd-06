using RoomReservationApi.Models;

namespace RoomReservationApi.Repositories;

public interface IReservationRepository
{
    public Reservation GetIfExists(long reservationId);
    public List<Reservation> GetAll();
    public void DeleteReservation(long reservationId);
    public void AddReservation(Reservation reservation);
    public void UpdateRoomId(long id, long roomId);
    public void UpdateOrganizerName(long id,string  organizerName);
    public void UpdateTopic(long id, string topic);
    public void UpdateDate(long id, DateOnly date);
    public void UpdateStartTime(long id, TimeOnly startTime);
    public void UpdateEndTime(long id, TimeOnly endTime);
    public void UpdateStatus(long id, ReservationStatus status);
}