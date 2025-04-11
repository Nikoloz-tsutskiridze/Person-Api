using BasePerson.Core.Dtos;
using BasePerson.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using Person.Core.Domains;

namespace BasePerson.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PhonesRepository _phonesRepository;

        public PhonesController(AppDbContext context,PhonesRepository phonesRepository)
        {
            _context = context;
            _phonesRepository = phonesRepository;
        }

        // GET: api/Phones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetPhones()
        {
            var phones = await _phonesRepository.GetAll();
            return Ok(phones);
        }

        // GET: api/Phones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneDto>> GetPhone(int id)
        {
            var phone = await _phonesRepository.GetById(id);
            return phone is not null ? Ok(phone) : NotFound($"Phone with ID {id} not found.");
        }

        // POST: api/Phones
        [HttpPost]
        public async Task<ActionResult<Phone>> PostPhone(PhoneContentDto phoneDto)
        {
            var phoneId = await _phonesRepository.Create(phoneDto);
            return CreatedAtAction(nameof(GetPhone), new { id = phoneId }, phoneId);
        }

        // PUT: api/Phones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhone(int id, PhoneDto phoneDto)
        {
            var updated = await _phonesRepository.Update(id, phoneDto);
            return updated ? Ok() : NotFound($"Phone with ID {id} not found.");
        }

        // DELETE: api/Phones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            var deleted = await _phonesRepository.Delete(id);
            return deleted ? NoContent() : NotFound($"Phone with ID {id} not found.");
        }
    }
}
