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
            var miejscaDetails = await _miejscaDetailRepo.GetALL();
            var vm = _mapper.Map<List<MiejscaDetailViewModel >> (miejscaDetails);
            return View(vm);
        }
        public async Task<IActionResult> Create() 
        { 
            var koncerty = await _koncertRepo.GetALL();
            ViewBag.koncertyList = new SelectList(koncerty, "Id", "NazwaKoncertu");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreateMiejscaViewModel vm) 
        { 
            var model = _mapper.Map<MiejscaDetail>(vm);
            if(await _miejscaDetailRepo.CheckExist(vm.NumerMiejsca, vm.KoncertID))
            { 
            await _miejscaDetailRepo.Insert(model);
            }
            await _miejscaDetailRepo.Insert(model);
            return RedirectToAction("Index");
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
