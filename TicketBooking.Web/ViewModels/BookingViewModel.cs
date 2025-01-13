using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Web.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string KoncertImage { get; set; }
        public string NazwaKoncertu { get; set; }
        [DataType(DataType.Date)]
        public DateTime KoncertDate { get; set; }
        public List<CheckBoxTable> SiedzenieDetail { get; set; } = new List<CheckBoxTable>();
    }
    public class CheckBoxTable 
    { 
        public int Id { get; set; }
        public int NumerMiejsca { get; set; }
        public string MiejsceImage { get; set; }
        public bool IsChecked { get; set; }
    }
}
