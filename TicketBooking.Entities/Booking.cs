using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int MiejscaDetailsID { get; set; }
        public MiejscaDetails MiejscaDetails { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date {  get; set; }
        public string VIP   { get; set; }
    }
}
