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
    public class MiejscaDetailsRepo : IMiejscaDetailsRepo
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor z Dependency Injection
        public MiejscaDetailsRepo(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CheckExist(int numerMiejsca, int koncertID)
        {
            return await _context.MiejscaKoncertDetails.AnyAsync(x=>x.NumerMiejsca==numerMiejsca && x.KoncertID==koncertID);
        }

        // Usunięcie miejsca
        public async Task Delete(MiejscaDetail miejsce)
        {
            _context.MiejscaKoncertDetails.Remove(miejsce);
            await _context.SaveChangesAsync();
        }

        // Pobranie wszystkich miejsc
        public async Task<IEnumerable<MiejscaDetail>> GetALL()
        {
            return await _context.MiejscaKoncertDetails
                .Include(x => x.Koncert) // Ładowanie powiązanego Koncert
                .ToListAsync();
        }

        // Pobranie miejsca po ID
        public async Task<MiejscaDetail> GetByID(int id)
        {
            return await _context.MiejscaKoncertDetails
                .Include(x => x.Koncert) // Ładowanie powiązanego Koncert
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // Dodanie nowego miejsca
        public async Task Insert(MiejscaDetail miejsce)
        {
            await _context.MiejscaKoncertDetails.AddAsync(miejsce);
            await _context.SaveChangesAsync();
        }

        // Aktualizacja miejsca
        public async Task Update(MiejscaDetail miejsce)
        {
            _context.MiejscaKoncertDetails.Update(miejsce);
            await _context.SaveChangesAsync();
        }
    }
}
