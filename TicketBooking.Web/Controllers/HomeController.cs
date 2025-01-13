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
        private IKupBiletRepo _kupBiletRepo;


        public HomeController(ILogger<HomeController> logger, IKoncertRepo koncertRepo, IMapper mapper, IKupBiletRepo kupBiletRepo)
        {
            _logger = logger;
            _koncertRepo = koncertRepo;
            _mapper = mapper;
            _kupBiletRepo = kupBiletRepo;
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
            var koncertInfo = await _koncertRepo.GetByID(id);
            var kupbilet = await _kupBiletRepo.GetTodaysKupBilet(koncertInfo.Id);

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
