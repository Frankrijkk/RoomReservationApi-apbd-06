using RoomReservationApi.Models;

namespace RoomReservationApi;

public class RoomRepository
{
    private List<Room> _rooms;

    public RoomRepository()
    {
        _rooms = new  List<Room>();
    }
    public List<Room> GetAllRooms()
    {
        return _rooms;
    }

    public Room? GetById(long id)
    {
        return _rooms.Where(r=>r.Id == id).FirstOrDefault();
    }

    public List<Room>? GetByBuildingCode(string buildingCode)
    {
        return _rooms.Where(r=>r.BuildingCode == buildingCode).ToList();
    }

    public bool AddRoom(Room room)
    {
        _rooms.Add(room);
        return true;
    }

    public bool DeleteRoom(Room room)
    {
        if(_rooms.Remove(room))
            return true;
        return false;
    }

    public bool UpdateRoom(long id, string? name, string? buildingCode, int? floor, bool? hasProjector, bool? isActive)
    {
        
    }
    
}