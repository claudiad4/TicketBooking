using TicketBooking.Entities;

namespace TicketBooking.Web.Controllers
{
    public class Booking
    {
        public int Id { get; set; }
        public int MiejscaDetailsId {  get; set; }
        public MiejscaDetails MiejscaDetails { get; set; }
    }
}
