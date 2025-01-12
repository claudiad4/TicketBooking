using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketBooking.Entities;

namespace TicketBooking.Web.ViewModels.MiejscaVM
{
    public class CreateMiejscaViewModel
    {
        [Range(1, 200)]
        public int NumerMiejsca { get; set; }
        public int KoncertID { get; set; }
        public string NazwaKoncertu { get; set; }
        public StatusMiejsca StatusMiejsca { get; set; }
    }
}
