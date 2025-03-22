using BasePerson.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using Person.Api.Domains;

namespace BasePerson.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomer()
        {
            var people = await _context.People
                .Select(x => x.ConvertToDto())
                .ToListAsync();

            return Ok(people);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var person = await _context.People
                .Where(x => x.Id == id)
                .Select(x => x.ConvertToDto())
                .FirstOrDefaultAsync();

            if (person == null) return NotFound();
            return Ok(person);
        }


        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customerDto)
        {
            var cityExists = await _context.Cities.AnyAsync(c => c.Id == customerDto.CityId);
            if (!cityExists)
                return BadRequest("Invalid CityId: City does not exist.");

            var person = new Customer
            {
                Name = customerDto.Name,
                LastName = customerDto.LastName,
                Gender = (Gender)customerDto.Gender,
                PersonalNumber = customerDto.PersonalNumber,
                DateOfBirth = customerDto.DateOfBirth,
                CityId = customerDto.CityId,
                Img = customerDto.Img
            };

            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = person.Id });
        }

        //[HttpPost]
        //public async Task<>

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDto customerDto)
        {
            var existingPerson = await _context.People.FindAsync(id);
            if (existingPerson == null) return NotFound();

            existingPerson.Name = customerDto.Name;
            existingPerson.LastName = customerDto.LastName;
            existingPerson.Gender = (Gender)customerDto.Gender;
            existingPerson.PersonalNumber = customerDto.PersonalNumber;
            existingPerson.DateOfBirth = customerDto.DateOfBirth;
            existingPerson.Img = customerDto.Img;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null) return NotFound();

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
