using System.Data;
using RoomReservationApi.Models;

namespace RoomReservationApi.Repositories;

public class RoomRepository : IRoomRepository
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
    

    public List<Room>? GetByBuildingCode(string buildingCode)
    {
        return _rooms.Where(r=>r.BuildingCode == buildingCode).ToList();
    }

    public void AddRoom(Room room)
    {
        if (_rooms.Contains(room))
        {
            throw new ArgumentException("Room Already exists");
        }
        _rooms.Add(room);
    }

    public void DeleteRoom(long id)
    {
        Room room = GetRoomIfExists(id);
        _rooms.Remove(room);
    }

    public void UpdateRoomName(long id, string name)
    {
        Room room = GetRoomIfExists(id);
        room.Name = name;
    }

    public void UpdateRoomBuildingCode(long id, string buildingCode)
    {
        Room room = GetRoomIfExists(id);
        room.BuildingCode = buildingCode;
    }

    public void UpdateRoomFloor(long id, int floor)
    {
        Room room = GetRoomIfExists(id);
        room.Floor = floor;
    }

    public void UpdateRoomCapacity(long id, int capacity)
    {
        Room room = GetRoomIfExists(id);
        room.Capacity = capacity;
    }

    public void UpdateRoomProjector(long id, bool hasProjector)
    {
        Room room =GetRoomIfExists(id);
        room.HasProjector = hasProjector;
    }

    public void UpdateRoomActive(long id, bool isActive)
    {
        Room room = GetRoomIfExists(id);
        room.IsActive = isActive;
    }

    public Room GetRoomIfExists(long id)
    {
        if (!_rooms.Select((room) => room.Id).Contains(id))
        {
            throw new ArgumentException($"Room {id} does not exist");
        }
        return _rooms.Where(r=>r.Id == id).Single();
    }
}