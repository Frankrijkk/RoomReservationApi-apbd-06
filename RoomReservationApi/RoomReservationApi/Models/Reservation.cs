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
    public long RoomId { get; set; }
    public string OrganizerName {get; set;}
    public string Topic { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public ReservationStatus Status { get; set; }
    
}