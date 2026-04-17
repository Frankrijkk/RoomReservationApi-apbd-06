namespace RoomReservationApi.Models;

public class Reservation
{
    public Reservation(long roomId, string organizerName, string topic, DateOnly date, TimeOnly startTime, TimeOnly endTime, ReservationStatus status)
    {
        Id = _currentId++;
        RoomId = roomId;
        OrganizerName = organizerName;
        Topic = topic;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
    }

    private static long _currentId = 0;
    public long Id { get; private set; }
    public long RoomId { get; private set; }
    public string OrganizerName {get; private set;}
    public string Topic { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public ReservationStatus Status { get; private set; }
    
}