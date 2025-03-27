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
                    Type = x.Type,
                    Number = x.Number
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
                    Type = x.Type,
                    Number = x.Number
                })
                .FirstOrDefaultAsync();

            if (phone == null) return NotFound();

            return phone;
        }

        // POST: api/Phones
        [HttpPost]
        public async Task<ActionResult<Phone>> PostPhone(PhoneContentDto phoneDto)
        {
            var existingPhone = await _context.Phones.AnyAsync(x => x.Number == phoneDto.Number);

            if (existingPhone) return BadRequest($"A phone with the number {phoneDto.Number} already exists.");

            var phone = new Phone
            {
                Type = phoneDto.Type,
                Number = phoneDto.Number,
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

            var phoneExists = await _context.Phones
            .AnyAsync(x => x.Number == phoneDto.Number && x.Id != id);

            if (phoneExists)
            {
                return BadRequest($"A phone with the number {phoneDto.Number} already exists.");
            }

            phone.Type = phoneDto.Type;
            phone.Number = phoneDto.Number;

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
