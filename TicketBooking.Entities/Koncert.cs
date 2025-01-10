using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class Koncert
    {
        public int Id { get; set; }
        public string NumerAutobusu { get; set; }
        public string MaksymalnaIloscSiedzen {  get; set; }
        public string BusImage { get; set; }
        public ICollection<SiedzeniaDetails> SiedzeniaDetails { get; set; } 
            = new List<SiedzeniaDetails>();
    }
}
