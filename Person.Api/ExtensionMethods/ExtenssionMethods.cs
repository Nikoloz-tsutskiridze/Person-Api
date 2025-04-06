using BasePerson.Api.Domains;
using BasePerson.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BasePerson.Api.ExtensionMethods
{
    public static class ExtenssionMethods
    {
        public static async Task<IEnumerable<PhoneRelativePersonDto>> GetById(this IQueryable<PhoneRelativePerson> phoneRelativePeople)
        {
            return await phoneRelativePeople.Select(x => new PhoneRelativePersonDto
            {
                Id = x.Id,
                PersonId = x.PersonId,
                PhoneId = x.PhoneId
            }).ToListAsync();
        }
    }
}
