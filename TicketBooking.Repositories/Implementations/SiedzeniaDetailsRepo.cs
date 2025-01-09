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
    public class SiedzeniaDetailsRepo : ISiedzeniaDetailsRepo
    {
        private readonly ApplicationDbContext _context;
        public async Task Delete(SiedzeniaDetails siedzenie)
        {
            _context.SiedzeniaBusDetails.Remove(siedzenie);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SiedzeniaDetails>> GetALL()
        {
            return await _context.SiedzeniaBusDetails.ToListAsync();
        }

        public async Task<SiedzeniaDetails> GetByID(int id)
        {
            return await _context.SiedzeniaBusDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(SiedzeniaDetails siedzenie)
        {
            await _context.SiedzeniaBusDetails.AddAsync(siedzenie);
            await _context.SaveChangesAsync();
        }

        public Task Update(SiedzeniaDetails siedzenie)
        {
            throw new NotImplementedException();
        }
    }
}
