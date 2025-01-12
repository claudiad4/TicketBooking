using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TicketBooking.Entities;

namespace TicketBooking.Web.ViewModels.MiejscaVM
{
    public class MiejscaDetailViewModel
    {
        public int Id { get; set; }
        public int NumerMiejsca { get; set; }
        public string NazwaKoncertu { get; set; }
        public string StatusMiejsca { get; set; }
    }
}
