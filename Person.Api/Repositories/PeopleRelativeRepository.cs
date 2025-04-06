using BasePerson.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using BasePerson.Api.Domains;
using System.Linq.Expressions;
namespace BasePerson.Api.Repositories
{
    public class PeopleRelativeRepository : BaseRepository
    {
        public PeopleRelativeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<ExsitingPeopleRelativeDto>> GetAll()
        {
            return await _appDbContext.PeopleRelative
                .Select(x => x.ConvertToDto())
                .ToListAsync();
        }

        private async Task<IEnumerable<ExsitingPeopleRelativeDto>> GetById(Expression<Func<PeopleRelative, bool>> filterOption)
        {
            var relations = await _appDbContext.PeopleRelative
                .Where(filterOption)
                .Select(x => x.ConvertToDto())
                .ToListAsync();
            return relations;
        }

        public async Task<IEnumerable<ExsitingPeopleRelativeDto>> GetById(int personId)
        {
            var relationsForFirst = await GetById(x => x.FirstPersonId == personId);
            var relationsForSecond = await GetById(x => x.SecondPersonId == personId);

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
