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
    public class KoncertRepo : IKoncertRepo
    {
        private readonly ApplicationDbContext _context;

        public KoncertRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Koncert koncert)
        {
            _context.KoncertInfo.Remove(koncert);
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

        public async Task Insert(Koncert koncert)
        {
            await _context.KoncertInfo.AddAsync(koncert);
            await _context.SaveChangesAsync();
        }

        public Task Update(Koncert koncert)
        {
            throw new NotImplementedException();
        }
    }
}
