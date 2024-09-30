using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;


namespace gwiBack.Application.Services
{
    public class ProfessionalInformationService : IProfessionalInformationService
    {
        private readonly IProfessionalInformationRepository _professionalInformationRepository;

        public ProfessionalInformationService(IProfessionalInformationRepository professionalInformationRepository)
        {
            _professionalInformationRepository = professionalInformationRepository ?? throw new ArgumentNullException(nameof(professionalInformationRepository));
        }

        public async Task<ProfessionalInformation> GetProfessionalInformationByIdAsync(Guid id)
        {
            var information = await _professionalInformationRepository.GetByIdAsync(id);

            if (information == null)
            {
                throw new KeyNotFoundException("Informação profissional não encontrada.");
            }

            return information;
        }

        public async Task<ProfessionalInformation> CreateProfessionalInformationAsync(ProfessionalInformation information)
        {
            if (information == null)
            {
                throw new ArgumentNullException(nameof(information));
            }

            return await _professionalInformationRepository.CreateAsync(information);
        }

        public async Task<ProfessionalInformation> UpdateProfessionalInformationAsync(ProfessionalInformation information)
        {
            if (information == null)
            {
                throw new ArgumentNullException(nameof(information));
            }

            var existingInformation = await _professionalInformationRepository.GetByIdAsync(information.Id);

            if (existingInformation == null)
            {
                throw new KeyNotFoundException("Informação profissional não encontrada para atualização.");
            }

            return await _professionalInformationRepository.UpdateAsync(information);
        }

        public async Task DeleteProfessionalInformationAsync(Guid id)
        {
            var information = await _professionalInformationRepository.GetByIdAsync(id);

            if (information == null)
            {
                throw new KeyNotFoundException("Informação profissional não encontrada para exclusão.");
            }

            await _professionalInformationRepository.DeleteAsync(id);
        }
    }
}