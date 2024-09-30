using gwiBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwiBack.Domain.Interfaces
{
    public interface IProfessionalSkillRepository
    {
        Task<IEnumerable<ProfessionalSkill>> GetAllAsync();
        Task<ProfessionalSkill?> GetByIdAsync(Guid id);
        Task<ProfessionalSkill> CreateAsync(ProfessionalSkill skill);
        Task<ProfessionalSkill> UpdateAsync(ProfessionalSkill skill);
        Task DeleteAsync(Guid id);
    }
}
