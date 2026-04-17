using RoomReservationApi.Exceptions;
using RoomReservationApi.Models;
using RoomReservationApi.Repositories;

namespace RoomReservationApi.Services;

public class RoomService : IRoomService
{
    
    private IRoomRepository _roomRepository;
    private IReservationRepository _reservationRepository;

    public RoomService(IRoomRepository roomrepository,IReservationRepository reservationRepository)
    {
        _roomRepository = roomrepository;
        _reservationRepository = reservationRepository;
    }
    public List<Room> GetAll(int? minCapacity,bool? hasProjector,bool? isActive)
    {
        List<Room> rooms= _roomRepository.GetAllRooms();
        if (minCapacity!=null)
        {
            rooms = rooms.Where(room=>room.Capacity>=minCapacity.Value).ToList();
        }

        if (hasProjector != null)
        {
            rooms = rooms.Where(room => room.HasProjector == hasProjector).ToList();
        }

        if (isActive != null)
        {
            rooms = rooms.Where(room => room.IsActive == isActive).ToList();

        }
        return rooms;
    }

    public Room GetById(long id)
    {
        return _roomRepository.GetRoomIfExists(id);
    }

    public long Create(string name, string buildingCode, int floor, int capacity, bool hadProjector, bool isActive)
    {
        Room room = new Room(name,buildingCode,floor,capacity,hadProjector,isActive);
        _roomRepository.AddRoom(room);
        return room.Id;
    }

    public void DeleteRoom(long id)
    {
        List<Reservation> reservations = _reservationRepository.GetAll().Where(r=>r.RoomId == id).Where(r=>r.Date>DateOnly.FromDateTime(DateTime.Now)).ToList();
        if (reservations.Count != 0)
        {
            throw new ReservationConflictException("There are Reservations for this room");
        }
        _roomRepository.DeleteRoom(id);
    }

    public List<Room> GetByBuildingCode(string buildingCode)
    {
        List<Room> rooms = _roomRepository.GetByBuildingCode(buildingCode);
        if (rooms.Count == 0)
        {
            throw new ArgumentException();
        }

        return rooms;
    }

    public void Update(long id, string? name, string? buildingCode, int? floor, int? capacity, bool? hasProjector, bool? isActive)
    {
        if (name != null)
        {
            _roomRepository.UpdateRoomName(id,name);
        }

        if (buildingCode != null)
        {
            _roomRepository.UpdateRoomBuildingCode(id,buildingCode);
        }

        if (floor != null)
        {
            _roomRepository.UpdateRoomFloor(id,floor.Value);
        }

        if (capacity != null)
        {
            _roomRepository.UpdateRoomCapacity(id,capacity.Value);
        }

        if (hasProjector != null)
        {
            _roomRepository.UpdateRoomProjector(id,hasProjector.Value);    
        }

        if (isActive != null)
        {
            _roomRepository.UpdateRoomActive(id,isActive.Value);
        }
    }
}