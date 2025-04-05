using BasePerson.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using Person.Api.Domains;

namespace BasePerson.Api.Repositories
{
    public class PhonesRepository : BaseRepository
    {
        public PhonesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<PhoneDto>> GetAll()
        {
            return await _appDbContext.Phones
               .Select(x => new PhoneDto
               {
                   Id = x.Id,
                   Type = x.Type,
                   Number = x.Number
               }).ToListAsync();
        }

        public async Task<PhoneDto?> GetById(int id)
        {
            return await _appDbContext.Phones
               .Where(x => x.Id == id)
               .Select(x => new PhoneDto
               {
                   Id = x.Id,
                   Type = x.Type,
                   Number = x.Number
               })
               .FirstOrDefaultAsync();
        }

        public async Task<int> Create(PhoneContentDto phoneDto)
        {
            var existingPhone = await _appDbContext.Phones.AnyAsync(x => x.Number == phoneDto.Number);
            if (existingPhone) throw new InvalidOperationException($"A phone with the number {phoneDto.Number} already exists.");

            var phone = new Phone
            {
                Type = phoneDto.Type,
                Number = phoneDto.Number,
            };

            _appDbContext.Phones.Add(phone);
            await _appDbContext.SaveChangesAsync();
            return phone.Id;
        }

        public async Task<bool> Update(int id, PhoneDto phoneDto)
        {
            var phone = await _appDbContext.Phones.FindAsync(id);
            if (phone == null) throw new InvalidOperationException();

            var phoneExists = await _appDbContext.Phones
            .AnyAsync(x => x.Number == phoneDto.Number && x.Id != id);

            if (phoneExists)
            {
                throw new InvalidOperationException
                    ($"A phone with the number {phoneDto.Number} already exists.");
            }

            phone.Type = phoneDto.Type;
            phone.Number = phoneDto.Number;

            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var phone = await _appDbContext.Phones.FindAsync(id);
            if (phone == null) return false;  

            _appDbContext.Phones.Remove(phone);
            await _appDbContext.SaveChangesAsync();
            return true;  
        }

    }
}
