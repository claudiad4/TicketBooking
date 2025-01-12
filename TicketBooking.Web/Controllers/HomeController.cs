using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Repositories.Implementations;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Web.Models;
using TicketBooking.Web.ViewModels.KoncertVM;

namespace TicketBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IKoncertRepo _koncertRepo;
        private IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IKoncertRepo koncertRepo, IMapper mapper)
        {
            _logger = logger;
            _koncertRepo = koncertRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var koncerty = await _koncertRepo.GetALL();
            var koncertViewModel = _mapper.Map<List<KoncertViewModel>>(koncerty);
            return View(koncertViewModel);
        }
        //[HttpGet]
        //public async Task <IActionResult> TicketBook(int id) 
        //{ 
        //var koncertInfo = 
        //}
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
