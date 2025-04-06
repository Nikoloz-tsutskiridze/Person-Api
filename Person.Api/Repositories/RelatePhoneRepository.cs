using BasePerson.Api.Domains;
using BasePerson.Api.Dtos;
using BasePerson.Api.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;

namespace BasePerson.Api.Repositories
{
    public class RelatePhoneRepository : BaseRepository
    {
        public RelatePhoneRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<PhoneRelativePersonDto>> GetAll()
        {
            return await _appDbContext.PhoneRelativePeople.AsQueryable().GetById();
        }
        public async Task<IEnumerable<PhoneRelativePersonDto>> GetById(int personId)
        {
            return await _appDbContext.PhoneRelativePeople
                .Where(x => x.PersonId == personId).GetById();
        }

        public async Task<PhoneRelativePerson> Create(PhoneRelativePersonDto phoneRelativePersonDto)
        {
            // exeftioni ori ertnairi kavshiri roar sheiqnas persons shoris

            var phoneRelativePerson = new PhoneRelativePerson
            {
                PersonId = phoneRelativePersonDto.PersonId,
                PhoneId = phoneRelativePersonDto.PhoneId
            };

            _appDbContext.PhoneRelativePeople.Add(phoneRelativePerson);
            await _appDbContext.SaveChangesAsync();
            return phoneRelativePerson;
        }

        public async Task<bool> RemovePhoneRelation(int id)
        {
            var relation = await _appDbContext.PhoneRelativePeople.SingleOrDefaultAsync(x => x.Id == id);
            if (relation == null) return false;

            _appDbContext.PhoneRelativePeople.Remove(relation);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
