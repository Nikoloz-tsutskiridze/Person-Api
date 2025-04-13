using BasePerson.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Person.Application.Interfaces;
using Person.Core.Domains;
using Person.Infrastructure.Repositories;

namespace Person.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityRepository _cityRepository;
        public CitiesController(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _cityRepository.GetAll();
            return Ok(cities);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _cityRepository.GetById(id);
            if (city == null) return NotFound();
            return Ok(city);
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(CityDto cityDto)
        {
            var city = await _cityRepository.Create(cityDto);
            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
        }

        // PUT: api/Cities/5
        [HttpPut]
        public async Task<IActionResult> PutCity(UpdateCityDto updateCityDto)
        {
            await _cityRepository.Update(updateCityDto);
            return Ok();
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _cityRepository.Delete(id);
            return Ok();
        }
    }
}
