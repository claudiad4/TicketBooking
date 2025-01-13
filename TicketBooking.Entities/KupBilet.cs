using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    internal class KupBilet
    {
        public int Id { get; set; }
        public int MiejscaDetailsId { get; set; }
        public MiejscaDetails MiejscaDetails { get; set}
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
