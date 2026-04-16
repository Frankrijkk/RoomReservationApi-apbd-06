using RoomReservationApi.Models;

namespace RoomReservationApi.Services;

public interface IRoomService
{
    public List<Room> GetAll();
    public Room GetById(long id);
    public void Create(string name,string buildingCode,int floor,int capacity,bool hadProjector, bool isActive);
    public void Update(long id, string? name, string? buildingCode,int? floor,int? capacity,bool? hasProjector,bool? isActive);
    public void DeleteRoom(long id);
    public List<Room> GetByBuildingCode(string buildingCode);
}