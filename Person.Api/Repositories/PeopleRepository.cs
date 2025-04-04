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
    public class PeopleRepository
    {
        private readonly AppDbContext _appDbContext;

        public PeopleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            var person = await _appDbContext.People
                .Where(x => x.Id == id)
                .Select(x => x.ConvertToDto())
                .FirstOrDefaultAsync();

            if (person == null) throw new InvalidOperationException("Person not found");

            var relativePhones = await _appDbContext.PhoneRelativePeople
                .Where(x => x.PersonId == id)
                .ToListAsync();

            var phoneContentDtos = new List<PhoneDetailsResponse>();
            foreach (var relativePhone in relativePhones)
            {
                var phone = await _appDbContext.Phones
                    .SingleOrDefaultAsync(x => x.Id == relativePhone.PhoneId);

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
            var cityExists = await _appDbContext.Cities.AnyAsync(c => c.Id == customerDto.CityId);
            if (!cityExists)
                throw new InvalidExpressionException($"Invalid CityId: City does not exist.");

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
            var existingPerson = await _appDbContext.People.FindAsync(customerDto.Id);
            if (existingPerson == null) throw new InvalidExpressionException("This person doesn't exists");

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
            var person = await _appDbContext.People.FindAsync(id);
            if (person == null) throw new InvalidExpressionException("Error deleting person");

            _appDbContext.People.Remove(person);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

    }
}
