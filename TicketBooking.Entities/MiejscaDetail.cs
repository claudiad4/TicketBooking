using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Entities
{
    public class MiejscaDetail
    {
        public int Id { get; set; }
        [Range(1,200)]
        public int NumerMiejsca { get; set; }
        public int KoncertID { get; set; }
        public Koncert Koncert { get; set; }
        public StatusMiejsca StatusMiejsca { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }

    public enum StatusMiejsca 
    { 
    Zarezerwowane,
    Dostępne,
    Niedostępne
    }
}

