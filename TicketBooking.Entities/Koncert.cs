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
        public string NumerKoncertu { get; set; }
        public string MaksymalnaIloscSiedzen {  get; set; }
        public string KoncertImage { get; set; }
        public ICollection<MiejscaDetails> SiedzeniaDetails { get; set; } 
            = new List<MiejscaDetails>();
    }
}
