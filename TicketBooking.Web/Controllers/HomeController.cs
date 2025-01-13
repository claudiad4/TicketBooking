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
            KupBiletViewModel vm = new KupBiletViewModel();

            // Pobranie informacji o koncercie
            var koncertInfo = await _koncertRepo.GetByID(id);

            // Pobranie listy kupionych biletów na dzisiejszy dzieñ
            var kupbilet = _kupBiletRepo.GetTodaysKupBilet(koncertInfo.Id)
                .GetAwaiter()
                .GetResult()
                .Select(x => x.MiejscaDetailsId)
                .ToList();

            // Ustawienie danych dla modelu widoku
            vm.KoncertImage = koncertInfo.KoncertImage;
            vm.NazwaKoncertu = koncertInfo.NazwaKoncertu;
            vm.KoncertDate = DateTime.Today;

            // Przetwarzanie szczegó³ów siedzeñ i dodanie ich do widoku
            foreach (var KoncertSiedzenie in koncertInfo.SiedzeniaDetails)
            {
                // Sprawdzenie, czy miejsce jest zajête
                bool isSeatOccupied = kupbilet.Contains(KoncertSiedzenie.Id);

                // Dodanie szczegó³ów miejsca do listy
                vm.SeatDetail.Add(new CheckBoxTable
                {
                    Id = KoncertSiedzenie.Id,
                    MiejsceImage = isSeatOccupied ? "RedChair.png" : "GreenChair.png",
                    IsChecked = isSeatOccupied
                });
            }

            // Przekazanie modelu widoku do widoku
            return View(vm);
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
