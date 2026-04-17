namespace RoomReservationApi.Exceptions;

public class ReservationConflictException : Exception
{
    public ReservationConflictException(string message) : base(message)
    {
    }
}