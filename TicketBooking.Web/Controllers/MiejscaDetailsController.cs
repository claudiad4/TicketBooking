using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketBooking.Entities;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Web.ViewModels.MiejscaVM;

namespace TicketBooking.Web.Controllers
{
    public class MiejscaDetailsController : Controller
    {
        private IMiejscaDetailsRepo _miejscaDetailRepo;
        private IKoncertRepo _koncertRepo;
        private IMapper _mapper;

        public MiejscaDetailsController(IMiejscaDetailsRepo miejscaDetailRepo, IKoncertRepo koncertRepo, IMapper mapper)
        {
            _miejscaDetailRepo = miejscaDetailRepo;
            _koncertRepo = koncertRepo;
            _mapper = mapper;
        }

        public async Task <IActionResult> Index(MiejscaDetailViewModel miejscaDetailViewModel)
        {
            //var miejscaDetails = await _miejscaDetailRepo.GetALL();
            //var vm = _mapper.Map<List<MiejscaDetailViewModel >> (miejscaDetails);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetSeatDetails()
        {
            try
            {
                // Pobranie parametrów od DataTables
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 0;
                int skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

                // Pobranie danych
                var seatDetails = await _miejscaDetailRepo.GetALL();

                // Filtrowanie danych (wyszukiwanie)
                if (!string.IsNullOrEmpty(searchValue))
                {
                    seatDetails = seatDetails.Where(m =>
                        (m.Koncert != null && m.Koncert.NazwaKoncertu.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        m.NumerMiejsca.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                        m.StatusMiejsca.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                    );
                }

                // Sortowanie danych
                if (!string.IsNullOrEmpty(sortColumnIndex) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    seatDetails = sortColumnIndex switch
                    {
                        "1" => sortColumnDir == "asc"
                            ? seatDetails.OrderBy(m => m.NumerMiejsca)
                            : seatDetails.OrderByDescending(m => m.NumerMiejsca),
                        "2" => sortColumnDir == "asc"
                            ? seatDetails.OrderBy(m => m.Koncert.NazwaKoncertu)
                            : seatDetails.OrderByDescending(m => m.Koncert.NazwaKoncertu),
                        "3" => sortColumnDir == "asc"
                            ? seatDetails.OrderBy(m => m.StatusMiejsca)
                            : seatDetails.OrderByDescending(m => m.StatusMiejsca),
                        _ => seatDetails
                    };
                }

                // Obliczanie ilości rekordów
                int recordsTotal = seatDetails.Count();

                // Stronicowanie
                var data = seatDetails.Skip(skip).Take(pageSize).ToList();

                // Mapowanie do ViewModel
                var vm = _mapper.Map<List<MiejscaDetailViewModel>>(data);

                // Przygotowanie danych JSON dla DataTables
                var jsonData = new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = vm
                };

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                // Obsługa błędów
                return StatusCode(500, new { error = ex.Message });
            }
        }



        [HttpGet]

        public async Task<IActionResult> Edit(int id) 
        { 
            var miejsceDetail = await _miejscaDetailRepo.GetByID(id);
            var koncerty = await _koncertRepo.GetALL();
            ViewBag.koncertyList = new SelectList(koncerty, "Id", "NazwaKoncertu");
            var vm = _mapper.Map<EditMiejscaDetailsViewModel> (miejsceDetail);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditMiejscaDetailsViewModel vm)
        {
            var model = await _miejscaDetailRepo.GetByID (vm.Id);
            
            if (!await _miejscaDetailRepo.CheckExist(vm.NumerMiejsca, vm.KoncertID))
            {
                model = _mapper.Map(vm, model);
                await _miejscaDetailRepo.Update(model);
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // Rozpakowanie obiektu z Task za pomocą await
            var miejscaDetail = await _miejscaDetailRepo.GetByID(id);

            if (miejscaDetail == null)
            {
                return NotFound(); // Jeśli obiekt nie istnieje, zwróć błąd 404
            }

            // Usunięcie rekordu
            await _miejscaDetailRepo.Delete(miejscaDetail);

            // Przekierowanie po usunięciu
            return RedirectToAction("Index");
        }


    }
}
