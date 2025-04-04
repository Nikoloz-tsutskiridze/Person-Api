using BasePerson.Api.Domains;
using BasePerson.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;

namespace BasePerson.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleRelativeController : Controller
    {
        private readonly AppDbContext _context;

        public PeopleRelativeController(AppDbContext context)
        {
            _context = context;
        }
        //GET: api/PeopleRelative
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneRelativePersonDto>>> GetPeopleRelative()
        {
            var relations = await _context.PeopleRelative
                .Select(x => new ExsitingPeopleRelativeDto
                {
                    Id = x.Id,
                    FirstPersonId = x.FirstPersonId,
                    SecondPersonId = x.SecondPersonId,
                    ConnectionType = x.ConnectionType,
                })
                .ToListAsync();

            return Ok(relations);
        }

        //GET: api/PeopleRelative/Person/1
        [HttpGet("Person/{personId}")]
        public async Task<ActionResult<IEnumerable<PhoneRelativePersonDto>>> GetPeopleRelativeById(int personId)
        {
            var relationsForFirst = await _context.PeopleRelative.Where(x => x.FirstPersonId == personId)
                .Select(x => new ExsitingPeopleRelativeDto
                {
                    Id = x.Id,
                    FirstPersonId = x.FirstPersonId,
                    SecondPersonId = x.SecondPersonId,
                    ConnectionType = x.ConnectionType,
                })
                .ToListAsync();

            var relationsForSecond = await _context.PeopleRelative.Where(x => x.SecondPersonId == personId)
               .Select(x => new ExsitingPeopleRelativeDto
               {
                   Id = x.Id,
                   FirstPersonId = x.FirstPersonId,
                   SecondPersonId = x.SecondPersonId,
                   ConnectionType = x.ConnectionType,
               })
               .ToListAsync();

            var unitedRelations = new List<PeopleRelativeDto>();

            unitedRelations.AddRange(relationsForFirst);
            unitedRelations.AddRange(relationsForSecond);

            if (!unitedRelations.Any()) return NotFound("No people relations found for this person.");

            return Ok(unitedRelations);
        }

        //POST: api/PeopleRelative 
        [HttpPost]
        public async Task<ActionResult<int>> RelatePersonToPerson(PeopleRelativeDto peopleRelativeDto)
        {
            var existingConnection = await _context.PeopleRelative
                .SingleOrDefaultAsync(x =>
                    (x.FirstPersonId == peopleRelativeDto.FirstPersonId && x.SecondPersonId == peopleRelativeDto.SecondPersonId) ||
                    (x.FirstPersonId == peopleRelativeDto.SecondPersonId && x.SecondPersonId == peopleRelativeDto.FirstPersonId) &&
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

            _context.PeopleRelative.Add(peopleRelative);
            await _context.SaveChangesAsync();
            return Ok(peopleRelative.Id);
        }


        //DELETE: api/PeopleRelative/5
        [HttpDelete]
        public async Task<IActionResult> RemovePeopleRelative(int Id)
        {
            var relation = await _context.PeopleRelative.SingleOrDefaultAsync(
                x => x.Id == Id);

            if (relation == null) return NotFound($"The connection doesn't exist {Id}.");

            _context.PeopleRelative.Remove(relation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
