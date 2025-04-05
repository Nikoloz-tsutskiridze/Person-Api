using BasePerson.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;
using Person.Api.Domains;
using System.Data;

namespace BasePerson.Api.Repositories
{
    public class CityRepository : BaseRepository
    {
        public CityRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _appDbContext.Cities.ToListAsync();
        }
        public async Task<City?> GetById(int id)
        {
            return await _appDbContext.Cities.FindAsync(id);
        }

        public async Task<City> Create(CityDto cityDto)
        {
            if (_appDbContext.Cities.Any(x => x.Name == cityDto.Name))
                throw new InvalidOperationException("This city already exists");

            var city = new City()
            {
                Name = cityDto.Name
            };

            _appDbContext.Cities.Add(city);
            await _appDbContext.SaveChangesAsync();

            return city;
        }

        public async Task Update(UpdateCityDto updateCityDto)
        {
            var existingCity = await GetById(updateCityDto.Id);

            if (existingCity == null)
                throw new InvalidOperationException($"City:{updateCityDto.Id} doesn't exist");

            existingCity.Name = updateCityDto.Name;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete (int id)
        {
            var city = await GetById(id);
            if (city == null) 
                throw new InvalidOperationException("City not found");

            _appDbContext.Cities.Remove(city);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Exist(int id)
        {
            var cityExists = await _appDbContext.Cities.AnyAsync(c => c.Id == id);
            if (!cityExists)
                throw new InvalidExpressionException($"Invalid CityId: City does not exist.");
        }
    }
}
