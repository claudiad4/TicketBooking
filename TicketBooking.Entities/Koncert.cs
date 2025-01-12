using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class Koncert
    {
        public int Id { get; set; }
        public required string NazwaKoncertu { get; set; }
        public required string Wykonawca { get; set; }
        public required string Opis { get; set; }
        [Range(1, 200)]
        public required int MaksymalnaIloscSiedzen {  get; set; }
        public string? KoncertImage { get; set; }
        
        public ICollection<MiejscaDetail> SiedzeniaDetails { get; set; }
            = new List<MiejscaDetail>();
    }
}
