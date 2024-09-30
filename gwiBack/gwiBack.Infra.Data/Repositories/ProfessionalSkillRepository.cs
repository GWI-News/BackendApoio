using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;
using gwiBack.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace gwiBack.Infra.Data.Repositories
{
    public class ProfessionalSkillRepository : IProfessionalSkillRepository
    {
        private readonly gwiDbContext _context;

        public ProfessionalSkillRepository(gwiDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProfessionalSkill> CreateAsync(ProfessionalSkill skill)
        {
            await _context.ProfessionalSkills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task DeleteAsync(Guid id)
        {
            var skill = await GetByIdAsync(id);
            if (skill != null)
            {
                _context.ProfessionalSkills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProfessionalSkill>> GetAllAsync()
        {
            return await _context.ProfessionalSkills.ToListAsync();
        }

        public async Task<ProfessionalSkill> GetByIdAsync(Guid id)
        {
            return await _context.ProfessionalSkills.FindAsync(id);
        }

        public async Task<ProfessionalSkill> UpdateAsync(ProfessionalSkill skill)
        {
            _context.ProfessionalSkills.Update(skill);
            await _context.SaveChangesAsync();
            return skill;
        }
    }
}
