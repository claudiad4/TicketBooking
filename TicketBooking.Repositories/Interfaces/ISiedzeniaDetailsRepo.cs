using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories.Interfaces
{
    public interface ISiedzeniaDetailsRepo
    {
        Task<IEnumerable<SiedzeniaDetails>> GetALL();
        Task<SiedzeniaDetails> GetByID(int id);
        Task Insert(SiedzeniaDetails siedzenie);
        Task Update(SiedzeniaDetails siedzenie);
        Task Delete(SiedzeniaDetails siedzenie);
    }
}
