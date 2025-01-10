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
    public class BusRepo : IBusRepo
    {
        private readonly ApplicationDbContext _context;

        public BusRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Koncert bus)
        {
            _context.KoncertInfo.Remove(bus);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Koncert>> GetALL()
        {
            return await _context.KoncertInfo.ToListAsync();
        }

        public async Task<Koncert> GetByID(int id)
        {
            return await _context.KoncertInfo.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(Koncert bus)
        {
            await _context.KoncertInfo.AddAsync(bus);
            await _context.SaveChangesAsync();
        }

        public Task Update(Koncert bus)
        {
            throw new NotImplementedException();
        }
    }
}
