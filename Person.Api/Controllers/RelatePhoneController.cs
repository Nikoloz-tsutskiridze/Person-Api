using BasePerson.Api.Domains;
using BasePerson.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;

namespace BasePerson.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatePhoneController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelatePhoneController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/RelatePhone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneRelativePersonDto>>> GetPhoneRelations()
        {
            var relations = await _context.PhoneRelativePeople
                .Select(x => new PhoneRelativePersonDto
                {
                    Id = x.Id,
                    PersonId = x.PersonId,
                    PhoneId = x.PhoneId
                })
                .ToListAsync();

            return Ok(relations);
        }

        //GET: api/RelatePhone/Person/1
        [HttpGet("Person/{personId}")]
        public async Task<ActionResult<IEnumerable<PhoneRelativePersonDto>>> GetPhoneRelationsById(int personId)
        {
            var relations = await _context.PhoneRelativePeople.Where(x => x.PersonId == personId)
                .Select(x => new PhoneRelativePersonDto
                {
                    Id = x.Id,
                    PersonId = x.PersonId,
                    PhoneId = x.PhoneId
                })
                .ToListAsync();

            if (!relations.Any()) return NotFound("No phone relations found for this person.");

            return Ok(relations);
        }

        //POST: api/RelatePhone
        [HttpPost]
        public async Task<ActionResult<int>> RelatePhoneToPerson(PhoneRelativePersonDto phoneRelativePersonDto)
        {
            var existingConnection = await _context.PhoneRelativePeople.SingleOrDefaultAsync(x =>
            x.PersonId == phoneRelativePersonDto.PersonId && x.PhoneId == phoneRelativePersonDto.PhoneId);

            if (existingConnection != null)
                throw new InvalidOperationException($"This connection already exists! personId:{phoneRelativePersonDto.PersonId} phoneId:{phoneRelativePersonDto.PhoneId}");

            var phoneRelativePerson = new PhoneRelativePerson
            {
                PersonId = phoneRelativePersonDto.PersonId,
                PhoneId = phoneRelativePersonDto.PhoneId,
            };

            _context.PhoneRelativePeople.Add(phoneRelativePerson);
            await _context.SaveChangesAsync();
            return Ok(phoneRelativePerson.Id);

        }

        //DELETE: api/RelatePhone/5
        [HttpDelete]
        public async Task<IActionResult> RemovePhoneRelation(int Id)
        {
            var relation = await _context.PhoneRelativePeople.SingleOrDefaultAsync(
                x => x.Id == Id);

            if (relation == null) return NotFound($"The connection doesn't exist {Id}.");

            _context.PhoneRelativePeople.Remove(relation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
