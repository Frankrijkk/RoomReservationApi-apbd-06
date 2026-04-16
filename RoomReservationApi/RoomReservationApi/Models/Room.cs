namespace RoomReservationApi.Models;

public class Room
{
    public Room(string name, string buildingCode, int floor, int capacity, bool hasProjector, bool isActive)
    {
        Id = Interlocked.Increment(ref _currentId);
        Name = name;
        BuildingCode = buildingCode;
        Floor = floor;
        Capacity = capacity;
        HasProjector = hasProjector;
        IsActive = isActive;
    }

    private static long _currentId = 1;
    public long Id { get; private set; }
    public string Name { get; set; }
    public string BuildingCode { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector { get;  set; }
    public bool IsActive { get; set; }
}