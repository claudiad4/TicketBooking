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
            var koncertInfo = await _koncertRepo.GetByID(id); // Pobierz informacje o koncercie

            // Wywo³anie GetTodaysBooking z u¿yciem GetAwaiter().GetResult()
            var booking = _bookingRepo.GetTodaysBooking(koncertInfo.Id)
                .GetAwaiter().GetResult() // Wykonuje synchroniczne oczekiwanie
                .Select(x => x.MiejscaDetailsID) // Pobierz ID miejsc
                .ToList(); // Konwertuj na listê

            vm.KoncertImage = koncertInfo.KoncertImage;
            vm.NazwaKoncertu = koncertInfo.NazwaKoncertu;

            vm.KoncertDate = DateTime.Today; // Ustaw datê na dzisiejsz¹

            // Iteracja przez miejsca koncertu
            foreach (var Miejsce in koncertInfo.SiedzeniaDetails)
            {
                vm.SiedzenieDetail.Add(new CheckBoxTable
                {
                    Id = Miejsce.Id,
                    MiejsceImage = booking.Contains(Miejsce.Id) ? "GreyChair.png": "GreenChair.png",
                    IsChecked = booking.Contains(Miejsce.Id) // Zaznacz, jeœli miejsce jest zarezerwowane
                });
            }

            return View(vm); // Zwróæ widok z modelem
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
