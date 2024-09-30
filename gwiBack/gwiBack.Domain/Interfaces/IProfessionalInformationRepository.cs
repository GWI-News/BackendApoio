using gwiBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwiBack.Domain.Interfaces
{
    public interface IProfessionalInformationRepository
    {
        Task<ProfessionalInformation> GetByIdAsync(Guid id);
        Task<ProfessionalInformation> CreateAsync(ProfessionalInformation information);
        Task<ProfessionalInformation> UpdateAsync(ProfessionalInformation information);
        Task DeleteAsync(Guid id);
    }
}
