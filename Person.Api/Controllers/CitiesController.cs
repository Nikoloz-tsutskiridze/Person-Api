using BasePerson.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using Person.Api.Domains;

namespace Person.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null) return NotFound();
            return city;
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(CityDto cityDto)
        {
            var  sameNameCity = _context.Cities.SingleOrDefault(x => x.Name == cityDto.Name);
            if (sameNameCity != null)
                throw new InvalidOperationException("this city already exists");

            var city = new City()
            {
                Name = cityDto.Name
            };
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
        }

        // PUT: api/Cities/5
        [HttpPut]
        public async Task<IActionResult> PutCity(UpdateCityDto updateCityDto)
        {
            var existingCity = _context.Cities.Find(updateCityDto.Id);

            if (existingCity == null)
                throw new InvalidOperationException($"City:{updateCityDto.Id} doesn't exist");

            existingCity.Name = updateCityDto.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null) return NotFound();
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
