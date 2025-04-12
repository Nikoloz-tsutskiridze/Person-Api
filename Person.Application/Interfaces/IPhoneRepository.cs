using BasePerson.Core.Dtos;
using Person.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Application.Interfaces
{
    public interface IPhoneRepository
    {
        Task<IEnumerable<PhoneDto>> GetAll();
        Task<PhoneDto?> GetById(int id);
        Task<int> Create(PhoneContentDto phoneDto);
        Task<bool> Update(int id, PhoneDto phoneDto);
        Task<bool> Delete(int id);
    }
}
