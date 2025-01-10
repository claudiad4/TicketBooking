using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories.Interfaces
{ 
    public interface IBusRepo
    {
        Task<IEnumerable<Koncert>> GetALL();
        Task<Koncert> GetByID(int id);
        Task Insert(Koncert bus);
        Task Update(Koncert bus);
        Task Delete(Koncert bus);
    }
}
