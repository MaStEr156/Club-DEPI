using Microsoft.EntityFrameworkCore;
using MVC_1_Depi.Data;
using MVC_1_Depi.Interfaces;
using MVC_1_Depi.Models;

namespace MVC_1_Depi.Repositories
{
    public class ClubRepo : IClubRepo
    {
        private readonly RunDbContext _context;

        public ClubRepo(RunDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Club club)
        {
            await _context.AddAsync(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }


        public async Task<IEnumerable<Club>> GetAllAsync()
        {
            return await _context.Clubs.Include(i => i.Address).ToListAsync();
        }


        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<Club?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<IEnumerable<Club?>> GetClubByCityAsync(string city)
        {
            return await _context.Clubs.Include(i => i.Address).Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
