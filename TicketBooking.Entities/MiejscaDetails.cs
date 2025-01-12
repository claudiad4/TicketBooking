using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class MiejscaDetails
    {
        public int Id { get; set; }
        [Range(1,200)]
        public int NumerMiejsca { get; set; }
        public int KoncertID { get; set; }
        public Koncert Koncert { get; set; }
        public StatusMiejsca StatusMiejsca { get; set; }

    }

    public enum StatusMiejsca 
    { 
    Zarezerwowane,
    Dostępne,
    Niedostępne
    }
}

