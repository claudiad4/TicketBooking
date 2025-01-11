using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Web.ViewModels.KoncertVM
{
    public class CreateKoncertViewModel
    {
        public required string NazwaKoncertu { get; set; }
        public required string Wykonawca { get; set; }
        public required string Opis { get; set; }
        [Range(1, 200)]
        public required int MaksymalnaIloscSiedzen { get; set; }
        public IFormFile? KoncertImage { get; set; }
    }
}
