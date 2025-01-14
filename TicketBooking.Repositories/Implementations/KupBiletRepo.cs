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
            var today = koncertData.Date;

            var kupbilet = await _context.KupBilet
                .Include(y => y.MiejscaDetails)
                .ThenInclude(z => z.Koncert)
                .Where(x => x.Data.Date == today && x.MiejscaDetails.KoncertID == koncertId)
                .ToListAsync();

            return kupbilet;
        }


        public async Task SaveBooking(List<KupBilet> kupBilets)
        {
            try
            {
                // Dodanie rezerwacji do tabeli KupBilet
                await _context.KupBilet.AddRangeAsync(kupBilets);

                // Pobranie ID miejsc do aktualizacji
                var miejscaIds = kupBilets.Select(b => b.MiejscaDetailsId).ToList();

                // Aktualizacja statusu miejsc w tabeli MiejscaKoncertDetails
                var miejscaDoAktualizacji = _context.MiejscaKoncertDetails
                    .Where(m => miejscaIds.Contains(m.Id))
                    .ToList();

                foreach (var miejsce in miejscaDoAktualizacji)
                {
                    miejsce.StatusMiejsca = 0; // 0 oznacza "Zarezerwowane"
                    Console.WriteLine($"Zaktualizowano miejsce ID={miejsce.Id} na StatusMiejsca=0");
                }

                // Zapisanie zmian w bazie
                await _context.SaveChangesAsync();
                Console.WriteLine("Rezerwacje i status miejsc zapisane pomyślnie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu rezerwacji: {ex.Message}");
                throw;
            }
        }


    }
}
