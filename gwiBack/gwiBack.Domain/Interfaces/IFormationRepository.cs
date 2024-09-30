using gwiBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwiBack.Domain.Interfaces
{
    public interface IFormationRepository
    {
        Task<IEnumerable<Formation>> GetAllAsync();
        Task<Formation?> GetByIdAsync(Guid id);
        Task<Formation> CreateAsync(Formation formation);
        Task<Formation> UpdateAsync(Formation formation);
        Task DeleteAsync(Guid id);
    }
}
