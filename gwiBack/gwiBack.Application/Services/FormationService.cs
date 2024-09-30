using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;

namespace gwiBack.Application.Services
{
    public class FormationService : IFormationService
    {
        private readonly IFormationRepository _formationRepository;

        public FormationService(IFormationRepository formationRepository)
        {
            _formationRepository = formationRepository ?? throw new ArgumentNullException(nameof(formationRepository));
        }

        public async Task<IEnumerable<Formation>> GetAllFormationsAsync()
        {
            return await _formationRepository.GetAllAsync();
        }

        public async Task<Formation> GetFormationByIdAsync(Guid id)
        {
            var formation = await _formationRepository.GetByIdAsync(id);

            if (formation == null)
            {
                throw new KeyNotFoundException("Formação não encontrada.");
            }

            return formation;
        }

        public async Task<Formation> CreateFormationAsync(Formation formation)
        {
            if (formation == null)
            {
                throw new ArgumentNullException(nameof(formation));
            }

            return await _formationRepository.CreateAsync(formation);
        }

        public async Task<Formation> UpdateFormationAsync(Formation formation)
        {
            if (formation == null)
            {
                throw new ArgumentNullException(nameof(formation));
            }

            var existingFormation = await _formationRepository.GetByIdAsync(formation.Id);

            if (existingFormation == null)
            {
                throw new KeyNotFoundException("Formação não encontrada para atualização.");
            }

            // Atualizar os detalhes diretamente
            existingFormation.Name = formation.Name;
            existingFormation.Institution = formation.Institution;
            existingFormation.StartDate = formation.StartDate;
            existingFormation.EndDate = formation.EndDate;
            existingFormation.Activity1 = formation.Activity1;
            existingFormation.Activity2 = formation.Activity2;
            existingFormation.Activity3 = formation.Activity3;

            return await _formationRepository.UpdateAsync(existingFormation);
        }


        public async Task DeleteFormationAsync(Guid id)
        {
            var formation = await _formationRepository.GetByIdAsync(id);

            if (formation == null)
            {
                throw new KeyNotFoundException("Formação não encontrada para exclusão.");
            }

            await _formationRepository.DeleteAsync(id);
        }
    }
}
