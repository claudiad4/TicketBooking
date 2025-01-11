using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories.Interfaces
{ 
    public interface IKoncertRepo
    {
        Task<IEnumerable<Koncert>> GetALL();
        Task<Koncert> GetByID(int id);
        Task Insert(Koncert koncert);
        Task Update(Koncert koncert);
        Task Delete(Koncert koncert);
    }
}
