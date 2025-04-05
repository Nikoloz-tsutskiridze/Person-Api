using BasePerson.Api.Dtos;
using BasePerson.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using Person.Api.Domains;
using System;
using System.Data;

namespace BasePerson.Api.Repositories
{
    public class PeopleService : BaseRepository
    {
        private readonly RelatePhoneRepository _relatePhoneRepository;
        private readonly PhonesRepository _phonesRepository;
        private readonly CityRepository _cityRepository;
        public PeopleService(AppDbContext appDbContext, 
            RelatePhoneRepository relatePhoneRepository,
            PhonesRepository phonesRepository,
            CityRepository cityRepository) : base(appDbContext)
        {
            _relatePhoneRepository = relatePhoneRepository;
            _phonesRepository = phonesRepository;
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<ExistingCustomerDto>> GetAll()
        {
            var people = await _appDbContext.People
                .Select(x => x.ConvertToDto())
                .ToListAsync();

            return people;
        }
        public async Task<CustomerDto> GetById(int id)
        {
            var customer = await GetCustomerFromDatabase(id);

            var person = customer.ConvertToDto();

            var relativePhones = await _relatePhoneRepository.GetById(id);

            var phoneContentDtos = new List<PhoneDetailsResponse>();
            foreach (var relativePhone in relativePhones)
            {
                var phone = await _phonesRepository.GetById(relativePhone.PhoneId);

                if (phone == null)
                    continue;

                var phoneContentDto = new PhoneDetailsResponse()
                {
                    Number = phone.Number,
                    Type = phone.Type,
                    PhoneId = phone.Id,
                    ConnectionId = relativePhone.Id
                };

                phoneContentDtos.Add(phoneContentDto);
            }

            person.Phones = phoneContentDtos;
            return person;
        }
        public async Task<CustomerDto> Create(CustomerDto customerDto)
        {
           await _cityRepository.Exist(customerDto.CityId); 

            var person = new Customer
            {
                Id = customerDto.Id,
                Name = customerDto.Name,
                LastName = customerDto.LastName,
                Gender = (Gender)customerDto.Gender,
                PersonalNumber = customerDto.PersonalNumber,
                DateOfBirth = customerDto.DateOfBirth,
                CityId = customerDto.CityId,
                Img = customerDto.Img
            };

            _appDbContext.People.Add(person);
            await _appDbContext.SaveChangesAsync();
            return person.ConvertToDto();
        }
        public async Task<bool> Update(ExistingCustomerDto customerDto)
        {
            var existingPerson = await GetCustomerFromDatabase(customerDto.Id);

            existingPerson.Name = customerDto.Name;
            existingPerson.LastName = customerDto.LastName;
            existingPerson.Gender = (Gender)customerDto.Gender;
            existingPerson.PersonalNumber = customerDto.PersonalNumber;
            existingPerson.DateOfBirth = customerDto.DateOfBirth;
            existingPerson.Img = customerDto.Img;

            await _appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var person = await GetCustomerFromDatabase(id);
            _appDbContext.People.Remove(person);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
        private async Task<Customer> GetCustomerFromDatabase(int id)
        {
            var person = await _appDbContext.People.FindAsync(id);
            if (person == null) throw new InvalidExpressionException("Error deleting person");
            return person;
        }

    }
}
