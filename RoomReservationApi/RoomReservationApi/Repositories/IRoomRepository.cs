using RoomReservationApi.Models;

namespace RoomReservationApi.Repositories;

public interface IRoomRepository
{
    public List<Room> GetAllRooms();
    public  Room GetRoomIfExists(long id);
    public List<Room> GetByBuildingCode(string buildingCode);
    public void AddRoom(Room room);
    public void DeleteRoom(long id);

    public void UpdateRoomName(long id, string name);
    public void UpdateRoomBuildingCode(long id, string buildingCode);
    public void UpdateRoomFloor(long id, int floor);
    public void UpdateRoomCapacity(long id, int capacity);
    public void UpdateRoomProjector(long id, bool hasProjector);
    public void UpdateRoomActive(long id, bool isActive);
}