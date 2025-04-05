using BasePerson.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using BasePerson.Api.Domains;
namespace BasePerson.Api.Repositories
{
    public class PeopleRelativeRepository
    {
        private readonly AppDbContext _appDbContext;

        public PeopleRelativeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<ExsitingPeopleRelativeDto>> GetAll()
        {
            return await _appDbContext.PeopleRelative
                .Select(x => new ExsitingPeopleRelativeDto
                {
                    Id = x.Id,
                    FirstPersonId = x.FirstPersonId,
                    SecondPersonId = x.SecondPersonId,
                    ConnectionType = x.ConnectionType,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ExsitingPeopleRelativeDto>> GetById(int personId)
        {
            var relationsForFirst = await _appDbContext.PeopleRelative
                 .Where(x => x.FirstPersonId == personId)
                 .Select(x => new ExsitingPeopleRelativeDto
                 {
                     Id = x.Id,
                     FirstPersonId = x.FirstPersonId,
                     SecondPersonId = x.SecondPersonId,
                     ConnectionType = x.ConnectionType,
                 })
                 .ToListAsync();

            var relationsForSecond = await _appDbContext.PeopleRelative
                .Where(x => x.SecondPersonId == personId)
                .Select(x => new ExsitingPeopleRelativeDto
                {
                    Id = x.Id,
                    FirstPersonId = x.FirstPersonId,
                    SecondPersonId = x.SecondPersonId,
                    ConnectionType = x.ConnectionType,
                })
                .ToListAsync();

            return relationsForFirst.Concat(relationsForSecond).ToList();
        }

        public async Task<int> Create(PeopleRelativeDto peopleRelativeDto)
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

        public async Task<bool> Delete(int Id)
        {
            var relation = await _appDbContext.PeopleRelative.SingleOrDefaultAsync(
                x => x.Id == Id);

            if (relation == null) throw new InvalidOperationException($"The connection doesn't exist {Id}.");

            _appDbContext.PeopleRelative.Remove(relation);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
