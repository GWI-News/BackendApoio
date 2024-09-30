using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;


namespace gwiBack.Application.Services
{
    public class ProfessionalSkillService : IProfessionalSkillService
    {
        private readonly IProfessionalSkillRepository _professionalSkillRepository;

        public ProfessionalSkillService(IProfessionalSkillRepository professionalSkillRepository)
        {
            _professionalSkillRepository = professionalSkillRepository ?? throw new ArgumentNullException(nameof(professionalSkillRepository));
        }

        public async Task<IEnumerable<ProfessionalSkill>> GetAllProfessionalSkillsAsync()
        {
            return await _professionalSkillRepository.GetAllAsync();
        }

        public async Task<ProfessionalSkill> GetProfessionalSkillByIdAsync(Guid id)
        {
            var skill = await _professionalSkillRepository.GetByIdAsync(id);

            if (skill == null)
            {
                throw new KeyNotFoundException("Habilidade profissional não encontrada.");
            }

            return skill;
        }

        public async Task<ProfessionalSkill> CreateProfessionalSkillAsync(ProfessionalSkill skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }

            return await _professionalSkillRepository.CreateAsync(skill);
        }

        public async Task<ProfessionalSkill> UpdateProfessionalSkillAsync(ProfessionalSkill skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }

            var existingSkill = await _professionalSkillRepository.GetByIdAsync(skill.Id);

            if (existingSkill == null)
            {
                throw new KeyNotFoundException("Habilidade profissional não encontrada para atualização.");
            }

            return await _professionalSkillRepository.UpdateAsync(skill);
        }

        public async Task DeleteProfessionalSkillAsync(Guid id)
        {
            var skill = await _professionalSkillRepository.GetByIdAsync(id);

            if (skill == null)
            {
                throw new KeyNotFoundException("Habilidade profissional não encontrada para exclusão.");
            }

            await _professionalSkillRepository.DeleteAsync(id);
        }
    }
}
