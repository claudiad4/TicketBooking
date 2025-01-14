using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Entities;
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
        public async Task<IActionResult> GetSeatDetailsByDate(int koncertId, DateTime koncertDate)
        {
            try
            {
                var zakupioneMiejsca = await _kupBiletRepo.GetTodaysKupBilet(koncertId, koncertDate);
                var miejscaIds = zakupioneMiejsca.Select(x => x.MiejscaDetailsId).ToList();

                var koncert = await _koncertRepo.GetByID(koncertId);
                if (koncert == null)
                {
                    return NotFound("Koncert nie zosta³ znaleziony.");
                }

                var seatDetails = koncert.SiedzeniaDetails.Select(seat => new CheckBoxTable
                {
                    Id = seat.Id,
                    MiejsceImage = miejscaIds.Contains(seat.Id) ? "RedChair.png" : "GreenChair.png",
                    IsChecked = miejscaIds.Contains(seat.Id),
                    NumerMiejsca = seat.NumerMiejsca
                }).ToList();

                return Json(seatDetails);
            }
            catch (Exception ex)
            {
                return BadRequest($"Wyst¹pi³ b³¹d: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> TicketBook(int id)
        {
            try
            {
                KupBiletViewModel vm = new KupBiletViewModel();

                // Pobranie informacji o koncercie
                var koncertInfo = await _koncertRepo.GetByID(id);

                if (koncertInfo == null)
                {
                    return NotFound("Koncert nie zosta³ znaleziony.");
                }

                // Pobranie listy kupionych biletów
                var kupbilet = _kupBiletRepo.GetTodaysKupBilet(koncertInfo.Id, DateTime.Today)
                    .GetAwaiter()
                    .GetResult()
                    .Select(x => x.MiejscaDetailsId)
                    .ToList();

                // Ustawienie danych dla modelu widoku
                vm.KoncertImage = koncertInfo.KoncertImage;
                vm.NazwaKoncertu = koncertInfo.NazwaKoncertu;
                vm.KoncertDate = DateTime.Today;

                // Przetwarzanie szczegó³ów miejsc
                foreach (var koncertSeat in koncertInfo.SiedzeniaDetails)
                {
                    vm.SeatDetail.Add(new CheckBoxTable
                    {
                        Id = koncertSeat.Id,
                        MiejsceImage = ((StatusMiejsca)koncertSeat.StatusMiejsca) switch
                        {
                            StatusMiejsca.Zarezerwowane => "RedChair.png",     // 0
                            StatusMiejsca.Dostêpne => "GreenChair.png",     // 1
                            StatusMiejsca.Niedostêpne => "yellow.png", // 2
                            _ => "Unknown.png" // Dla nieznanego statusu
                        },
                        IsChecked = koncertSeat.StatusMiejsca == StatusMiejsca.Zarezerwowane,
                        NumerMiejsca = koncertSeat.NumerMiejsca
                    });
                }

                return View(vm);
            }
            catch (Exception ex)
            {
                // Obs³uga b³êdów
                return BadRequest($"Wyst¹pi³ b³¹d: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> TicketBook(KupBiletViewModel vm)
        {
         

            // Przygotowanie listy rezerwacji
            List<KupBilet> bookings = new List<KupBilet>();

            // Pobranie wybranych miejsc (zaznaczonych checkboxów)
            var selectedSeatIds = vm.SeatDetail.Where(x => x.IsChecked == true).Select(x => x.Id).ToList();
            var bookingDate = vm.KoncertDate;

            // Tworzenie rezerwacji dla ka¿dego wybranego miejsca
            foreach (var seatDetailId in selectedSeatIds)
            {
                bookings.Add(new KupBilet
                {
                    Data = bookingDate,
                    MiejscaDetailsId = seatDetailId
                });
            }

            // Zapisanie rezerwacji w repozytorium
            await _kupBiletRepo.SaveBooking(bookings);

            // Powiadomienie u¿ytkownika o sukcesie
            TempData["success"] = "Twoje bilety zosta³y pomyœlnie zarezerwowane!";
            return RedirectToAction("Index");
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
