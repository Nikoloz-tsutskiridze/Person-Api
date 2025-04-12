using BasePerson.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Person.Application.Interfaces;
using Person.Core.Domains;
using Person.Infrastructure.Data;
using System.Data;

namespace Person.Infrastructure.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        private const string CityAlreadyExists = "This city already exists";
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
                throw new InvalidOperationException(CityAlreadyExists);

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
            if (_appDbContext.Cities.Any(x => x.Name == updateCityDto.Name && x.Id != updateCityDto.Id))
                throw new InvalidOperationException(CityAlreadyExists);

            var existingCity = await GetById(updateCityDto.Id);

            if (existingCity == null)
                throw new InvalidOperationException($"City:{updateCityDto.Id} doesn't exist");

            if (existingCity.Name == updateCityDto.Name)
                return;

            existingCity.Name = updateCityDto.Name;
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Delete(int id)
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
