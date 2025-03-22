using BasePerson.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using Person.Api.Domains;

namespace BasePerson.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhonesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Phones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetPhones()
        {
            var phones = await _context.Phones
                .Select(x => new PhoneDto
                {
                    Id = x.Id,
                    Type = (int)x.Type,
                    Number = x.Number,
                    PersonId = x.PersonId
                }).ToListAsync();

            return Ok(phones);
        }

        // GET: api/Phones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneDto>> GetPhone(int id)
        {
            var phone = await _context.Phones
                .Where(x => x.Id == id)
                .Select(x => new PhoneDto
                {
                    Id = x.Id,
                    Type = (int)x.Type,
                    Number = x.Number,
                    PersonId = x.PersonId
                })
                .FirstOrDefaultAsync();

            if (phone == null) return NotFound();

            return phone;
        }

        // POST: api/Phones
        [HttpPost]
        public async Task<ActionResult<Phone>> PostPhone(PhoneDto phoneDto)
        {
            var phone = new Phone
            {
                Type = (PhoneType)phoneDto.Type,
                Number = phoneDto.Number,
                PersonId = phoneDto.PersonId
            };

            _context.Phones.Add(phone);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhone), new { id = phone.Id }, phone);
        }

        // PUT: api/Phones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhone(int id, PhoneDto phoneDto)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone == null) return NotFound();

            phone.Type = (PhoneType)phoneDto.Type;
            phone.Number = phoneDto.Number;
            phone.PersonId = phoneDto.PersonId;

            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Phones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone == null) return NotFound();

            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
