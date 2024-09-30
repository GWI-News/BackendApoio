using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;
using gwiBack.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace gwiBack.Infra.Data.Repositories
{
    public class FormationRepository : IFormationRepository
    {
        private readonly gwiDbContext _context;

        public FormationRepository(gwiDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Formation>> GetAllAsync()
        {
            return await _context.Formations.ToListAsync();
        }

        public async Task<Formation> GetByIdAsync(Guid id)
        {
            return await _context.Formations.FindAsync(id);
        }

        public async Task<Formation> CreateAsync(Formation formation)
        {
            await _context.Formations.AddAsync(formation);
            await _context.SaveChangesAsync();
            return formation;
        }

        public async Task<Formation> UpdateAsync(Formation formation)
        {
            _context.Formations.Update(formation);
            await _context.SaveChangesAsync();
            return formation;
        }

        public async Task DeleteAsync(Guid id)
        {
            var formation = await _context.Formations.FindAsync(id);
            if (formation != null)
            {
                _context.Formations.Remove(formation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
