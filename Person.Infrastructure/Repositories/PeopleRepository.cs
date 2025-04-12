using BasePerson.Core.Domains;
using BasePerson.Core.Dtos;
using BasePerson.Core.Responses;
using Person.Core.Domains;
using System.Data;
using Person.Application.Interfaces;
using Person.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Person.Infrastructure.Data;

namespace BasePerson.Api.Repositories
{
    public class PeopleService : BaseRepository , IPeopleRepository
    {
        private readonly PhonesRepository _phonesRepository;
        private readonly CityRepository _cityRepository;
        public PeopleService(AppDbContext appDbContext,
            PhonesRepository phonesRepository,
            CityRepository cityRepository) : base(appDbContext)
        {
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
        public async Task<ExistingCustomerDto> GetById(int id)
        {
            var customer = await GetCustomerFromDatabase(id);

            var person = customer.ConvertToDto();

            #region Phones
            var relativePhones = await _appDbContext.PhoneRelativePeople.Where(x => x.PersonId == id).ToListAsync();
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
            #endregion

            var relations = await _appDbContext.PeopleRelative
                .Where(x => x.FirstPersonId == id || x.SecondPersonId == id)
                .Select(x => new PersonDetailsResponse(
                    x.ConnectionType,
                    x.FirstPersonId == id ? x.SecondPersonId : x.FirstPersonId,
                    x.Id
                ))
                .ToListAsync();
            person.ConnectedPeople = relations;

            return person;
        }
        public async Task<int> ConnectPeople(PeopleRelativeDto peopleRelativeDto)
        {
            var existingConnection = await _appDbContext.PeopleRelative
                .SingleOrDefaultAsync(x =>
                   ((x.FirstPersonId == peopleRelativeDto.FirstPersonId && x.SecondPersonId == peopleRelativeDto.SecondPersonId) ||
                   (x.FirstPersonId == peopleRelativeDto.SecondPersonId && x.SecondPersonId == peopleRelativeDto.FirstPersonId)) &&
                   x.ConnectionType == peopleRelativeDto.ConnectionType

                );

            if (existingConnection != null)
                throw new InvalidOperationException($"This connection already exists! " +
                    $"First Person ID:{peopleRelativeDto.FirstPersonId} " +
                    $"Second Person ID:{peopleRelativeDto.SecondPersonId}");

            var peopleRelative = new PeopleRelative
            {
                FirstPersonId = peopleRelativeDto.FirstPersonId,
                SecondPersonId = peopleRelativeDto.SecondPersonId,
                ConnectionType = peopleRelativeDto.ConnectionType,
            };

            _appDbContext.PeopleRelative.Add(peopleRelative);
            await _appDbContext.SaveChangesAsync();

            return peopleRelative.Id;
        }
        public async Task<bool> DisconnectPeople(int connectionId)
        {
            var relation = await _appDbContext.PeopleRelative.SingleOrDefaultAsync(
               x => x.Id == connectionId);

            if (relation == null) throw new InvalidOperationException($"The connection doesn't exist {connectionId}.");

            _appDbContext.PeopleRelative.Remove(relation);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
        public async Task<ExistingCustomerDto> Create(CustomerDto customerDto)
        {
            await _cityRepository.Exist(customerDto.CityId);

            var person = new Customer
            {
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
            if (person == null)
                throw new InvalidOperationException($"Couldn't find person ID:{id}");
            return person;
        }
        public async Task<int> ConnectPhone(PhoneRelativePersonDto phoneRelativePersonDto)
        {
            // exeftioni ori ertnairi kavshiri roar sheiqnas persons shoris

            var phoneRelativePerson = new PhoneRelativePerson
            {
                PersonId = phoneRelativePersonDto.PersonId,
                PhoneId = phoneRelativePersonDto.PhoneId
            };

            _appDbContext.PhoneRelativePeople.Add(phoneRelativePerson);
            await _appDbContext.SaveChangesAsync();
            return phoneRelativePerson.Id;
        }
        public async Task<bool> DisconectPhone(int id)
        {
            var relation = await _appDbContext.PhoneRelativePeople.SingleOrDefaultAsync(x => x.Id == id);
            if (relation == null) return false;

            _appDbContext.PhoneRelativePeople.Remove(relation);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
