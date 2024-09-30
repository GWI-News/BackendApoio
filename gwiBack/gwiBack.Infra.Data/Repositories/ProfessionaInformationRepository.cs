using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;
using gwiBack.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace gwiBack.Infra.Data.Repositories
{
    public class ProfessionalInformationRepository : IProfessionalInformationRepository
    {
        private readonly gwiDbContext _context;

        public ProfessionalInformationRepository(gwiDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProfessionalInformation> CreateAsync(ProfessionalInformation information)
        {
            await _context.ProfessionalInformation.AddAsync(information);
            await _context.SaveChangesAsync();
            return information;
        }

        public async Task DeleteAsync(Guid id)
        {
            var information = await GetByIdAsync(id);
            if (information != null)
            {
                _context.ProfessionalInformation.Remove(information);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProfessionalInformation>> GetAllAsync()
        {
            return await _context.ProfessionalInformation.ToListAsync();
        }

        public async Task<ProfessionalInformation> GetByIdAsync(Guid id)
        {
            return await _context.ProfessionalInformation.FindAsync(id);
        }

        public async Task<ProfessionalInformation> UpdateAsync(ProfessionalInformation information)
        {
            _context.ProfessionalInformation.Update(information);
            await _context.SaveChangesAsync();
            return information;
        }
    }
}
