using Microsoft.AspNetCore.Mvc;

namespace RoomReservationApi.Controllers;

public class RoomsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}