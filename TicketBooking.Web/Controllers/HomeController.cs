using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Repositories.Implementations;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Web.Models;
using TicketBooking.Web.ViewModels;
using TicketBooking.Web.ViewModels.KoncertVM;

namespace TicketBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IKoncertRepo _koncertRepo;
        private IMapper _mapper;
        private IBookingRepo _bookingRepo;

        public HomeController(ILogger<HomeController> logger, IKoncertRepo koncertRepo, IMapper mapper, IBookingRepo bookingRepo)
        {
            _logger = logger;
            _koncertRepo = koncertRepo;
            _mapper = mapper;
            _bookingRepo = bookingRepo;
        }

        public async Task<IActionResult> Index()
        {
            var koncerty = await _koncertRepo.GetALL();
            var koncertViewModel = _mapper.Map<List<KoncertViewModel>>(koncerty);
            return View(koncertViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> TicketBook(int id)
        {
            BookingViewModel vm = new BookingViewModel();
            var koncertInfo = await _koncertRepo.GetByID(id);
            var booking = await _bookingRepo.GetTodaysBooking(koncertInfo.Id);
            vm.KoncertDate = DateTime.Today;
            foreach (var Miejsce in koncertInfo.MiejscaDetail)

            return View();

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
