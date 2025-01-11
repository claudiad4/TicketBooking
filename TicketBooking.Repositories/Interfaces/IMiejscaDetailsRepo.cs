using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories.Interfaces
{
    public interface IMiejscaDetailsRepo
    {
        Task<IEnumerable<MiejscaDetails>> GetALL();
        Task<MiejscaDetails> GetByID(int id);
        Task Insert(MiejscaDetails siedzenie);
        Task Update(MiejscaDetails siedzenie);
        Task Delete(MiejscaDetails siedzenie);
    }
}
