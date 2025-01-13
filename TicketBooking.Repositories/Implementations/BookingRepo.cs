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
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext _context;

        public BookingRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetTodaysBooking(int koncertid)
        {
            var today = DateTime.Today;

            // Asynchroniczne pobranie listy rezerwacji
            var bookings = await _context.Booking
                .Include(y => y.MiejscaDetails)
                .ThenInclude(z => z.Koncert)
                .Where(x => x.Date.Date == today && x.MiejscaDetails.KoncertID == koncertid)
                .ToListAsync();

            return bookings; // ToListAsync() już zwraca List<Booking>, która implementuje IEnumerable<Booking>
        }

    }
}
