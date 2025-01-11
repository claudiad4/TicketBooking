using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Entities;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Repositories;
using TicketBooking.Web.ViewModels.KoncertVM;

public class KoncertyController : Controller
{
    private readonly IKoncertRepo _koncertRepo;
    private IMapper _mapper;
    private IUtilityRepo _utilityRepo;
    private string KoncertImage = "KoncertImage";

    public KoncertyController(IKoncertRepo koncertRepo, IMapper mapper, IUtilityRepo utilityRepo)
    {
        _koncertRepo = koncertRepo;
        _mapper = mapper;
        _utilityRepo = utilityRepo;
    }

    public async Task<IActionResult> Index()
    {
        var koncerty = await _koncertRepo.GetALL();
        var vm = _mapper.Map<List<KoncertViewModel>>(koncerty);
        return View(vm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateKoncertViewModel vm)
    {
        var model = _mapper.Map<Koncert>(vm);
        if (vm.KoncertImage != null)
        {
            model.KoncertImage = await _utilityRepo.SaveImagePath(KoncertImage, vm.KoncertImage);
        }
        await _koncertRepo.Insert(model);
        TempData["Sukces"] = "Twój wpis został dodany.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var koncertDetail = await _koncertRepo.GetByID(id);
        var vm = _mapper.Map<EditKoncertViewModel>(koncertDetail);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditKoncertViewModel vm)
    {
        var koncert = await _koncertRepo.GetByID(vm.Id);

        // Jeśli przesłano plik obrazu, zapisz go i zaktualizuj ścieżkę
        if (vm.KoncertImageFile != null)
        {
            // KoncertImageFile nie powinno być przypisane do koncertu, ponieważ ta właściwość nie istnieje w modelu Koncert
            koncert.KoncertImage = await _utilityRepo.EditFilePath(KoncertImage, vm.KoncertImageFile, koncert.KoncertImage);
        }

        // Mapowanie pozostałych właściwości
        koncert = _mapper.Map(vm, koncert);

        await _koncertRepo.Update(koncert);
        return RedirectToAction("Index");
    }


    
}
