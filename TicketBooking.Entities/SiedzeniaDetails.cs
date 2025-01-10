using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class SiedzeniaDetails
    {
        public int Id { get; set; }
        public int NumerSiedzenia { get; set; }
        public int BusID { get; set; }
        public Koncert Bus { get; set; }
        public StatusSiedzenia StatusSiedzenia { get; set; }

    }

    public enum StatusSiedzenia 
    { 
    Zarezerwowane,
    Dostępne,
    Zniszczone
    }
}

