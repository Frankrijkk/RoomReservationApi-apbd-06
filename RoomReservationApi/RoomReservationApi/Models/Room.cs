namespace RoomReservationApi.Models;

public class Room
{
    public Room(string name, string buildingCode, int floor, bool hasProjector, bool isActive)
    {
        Id = Interlocked.Increment(ref _currentId);
        Name = name;
        BuildingCode = buildingCode;
        Floor = floor;
        HasProjector = hasProjector;
        IsActive = isActive;
    }

    private static long _currentId = 1;
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string BuildingCode { get; private set; }
    public int Floor { get; private set; }
    public bool HasProjector { get;  set; }
    public bool IsActive { get; private set; }
}