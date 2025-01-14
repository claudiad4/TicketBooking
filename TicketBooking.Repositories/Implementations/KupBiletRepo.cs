using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;
using TicketBooking.Repositories.Interfaces;

namespace TicketBooking.Repositories.Implementations
{
    public class KupBiletRepo : IKupBiletRepo
    {
        private readonly ApplicationDbContext _context;

        public KupBiletRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KupBilet>> GetTodaysKupBilet(int koncertId, DateTime koncertData)
        {
            var today = DateTime.Today;

            var kupbilet = await _context.KupBilet
                .Include(y => y.MiejscaDetails)
                .ThenInclude(z => z.Koncert) 
                .Where(x => x.Data.Date==today && 
                x.MiejscaDetails.KoncertID==koncertId)
                .ToListAsync();

            return kupbilet;
        }
    }
}
