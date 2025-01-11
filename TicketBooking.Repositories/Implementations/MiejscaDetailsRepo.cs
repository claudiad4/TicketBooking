using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;
using TicketBooking.Repositories.Interfaces;

namespace TicketBooking.Repositories.Implementations
{
    public class MiejscaDetailsRepo : IMiejscaDetailsRepo
    {
        private readonly ApplicationDbContext _context;
        public async Task Delete(MiejscaDetails miejsce)
        {
            _context.MiejscaKoncertDetails.Remove(miejsce);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MiejscaDetails>> GetALL()
        {
            return await _context.MiejscaKoncertDetails.ToListAsync();
        }

        public async Task<MiejscaDetails> GetByID(int id)
        {
            return await _context.MiejscaKoncertDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(MiejscaDetails miejsce)
        {
            await _context.MiejscaKoncertDetails.AddAsync(miejsce);
            await _context.SaveChangesAsync();
        }

        public Task Update(MiejscaDetails siedzenie)
        {
            throw new NotImplementedException();
        }
    }
}
