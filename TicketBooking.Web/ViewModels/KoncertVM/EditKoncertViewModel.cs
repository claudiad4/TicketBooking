using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Web.ViewModels.KoncertVM
{
    public class EditKoncertViewModel
    {
        public int Id { get; set; }
        public required string NazwaKoncertu { get; set; }
        public required string Wykonawca { get; set; }
        public required string Opis { get; set; }
        [Range(1, 200)]
        public required int MaksymalnaIloscSiedzen { get; set; }

        public IFormFile KoncertImageFile {get; set;} 
        public string KoncertImage { get; set; }
    }
}
