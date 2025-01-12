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
        Task<IEnumerable<MiejscaDetail>> GetALL();
        Task<MiejscaDetail> GetByID(int id);
        Task Insert(MiejscaDetail siedzenie);
        Task Update(MiejscaDetail siedzenie);
        Task Delete(MiejscaDetail siedzenie);
        Task<bool> CheckExist(int numerMiejsca, int koncertID);
    }
}
