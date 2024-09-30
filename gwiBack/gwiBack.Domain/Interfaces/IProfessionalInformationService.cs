using gwiBack.Domain.Entities;

namespace gwiBack.Domain.Interfaces
{
    public interface IProfessionalInformationService
    {
        Task<ProfessionalInformation> GetProfessionalInformationByIdAsync(Guid id);
        Task<ProfessionalInformation> CreateProfessionalInformationAsync(ProfessionalInformation information);
        Task<ProfessionalInformation> UpdateProfessionalInformationAsync(ProfessionalInformation information);
        Task DeleteProfessionalInformationAsync(Guid id);
    }
}
