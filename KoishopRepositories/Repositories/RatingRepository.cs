using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoishopRepositories.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private readonly KoishopContext _context;

        public RatingRepository(KoishopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Rating>> GetListAsync() 
        {
            return await _context.Ratings
                .Where(e => e.isDeleted == false || e.isDeleted == null)
                .Include(e => e.User)
                .AsNoTracking().ToListAsync(); 
        }
    }
}
