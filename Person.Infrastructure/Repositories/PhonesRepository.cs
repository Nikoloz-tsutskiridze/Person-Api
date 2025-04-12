using BasePerson.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Person.Application.Interfaces;
using Person.Core.Domains;
using Person.Infrastructure.Data;
using Person.Infrastructure.Repositories;

namespace BasePerson.Api.Repositories
{
    public class PhonesRepository : BaseRepository, IPhoneRepository
    {
        public PhonesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<IEnumerable<PhoneDto>> GetAll()
        {
            return await _appDbContext.Phones
               .Select(x => x.ConvertToDto())
               .ToListAsync();
        }
        public async Task<PhoneDto?> GetById(int id)
        {
            var phone = await _appDbContext.Phones
               .FirstOrDefaultAsync(x => x.Id == id);

            if (phone == null) throw new InvalidOperationException("Phone doesn't exists");

            var phoneDto = phone.ConvertToDto();
            return phoneDto;
        }
        public async Task<int> Create(PhoneContentDto phoneDto)
        {
            await Validation(phoneDto);

            var phone = new Phone
            {
                Type = phoneDto.Type,
                Number = phoneDto.Number,
            };

            _appDbContext.Phones.Add(phone);
            await _appDbContext.SaveChangesAsync();
            return phone.Id;
        }
        private async Task Validation(PhoneContentDto phoneDto)
        {
            var existingPhone = await _appDbContext.Phones.AnyAsync(x => x.Number == phoneDto.Number);
            if (existingPhone) throw new InvalidOperationException($"A phone with the number {phoneDto.Number} already exists.");

        }
        public async Task<bool> Update(int id, PhoneDto phoneDto)
        {
            var phone = await GetFromDatabase(phoneDto.Id);
            
            if (phone.Number == phoneDto.Number)
                throw new InvalidOperationException("This phone has already same number");

            await Validation(phoneDto);

            phone.Type = phoneDto.Type;
            phone.Number = phoneDto.Number;

            await _appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id)
        {
           var phone = await GetFromDatabase(id);

            _appDbContext.Phones.Remove(phone);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        private async Task<Phone> GetFromDatabase(int id)
        {
            var phone = await _appDbContext.Phones.FindAsync(id);
            if (phone == null) throw new InvalidOperationException();

            return phone;
        }
    }
}
