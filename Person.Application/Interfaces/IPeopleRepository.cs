using BasePerson.Core.Domains;
using BasePerson.Core.Dtos;
using BasePerson.Core.Responses;
using Person.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Application.Interfaces
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<ExistingCustomerDto>> GetAll();
        Task<ExistingCustomerDto> GetById(int id);
        Task<int> ConnectPeople(PeopleRelativeDto peopleRelativeDto);
        Task<bool> DisconnectPeople(int connectionId);
        Task<ExistingCustomerDto> Create(CustomerDto customerDto);
        Task<bool> Update(ExistingCustomerDto customerDto);
        Task<bool> Delete(int id);
        Task<int> ConnectPhone(PhoneRelativePersonDto phoneRelativePersonDto);
        Task<bool> DisconectPhone(int id);
    }
}
