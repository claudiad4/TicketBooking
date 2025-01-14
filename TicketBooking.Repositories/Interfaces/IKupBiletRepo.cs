using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories.Interfaces
{
    public interface IKupBiletRepo
    {
        Task<IEnumerable<KupBilet>> GetTodaysKupBilet(int koncertId, DateTime dataKoncertu);
        Task SaveBooking(List<KupBilet> booking);
    }
}
