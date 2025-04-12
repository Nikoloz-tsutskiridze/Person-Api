using BasePerson.Core.Dtos;
using Person.Core.Domains;
using System.Data;

namespace Person.Application.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAll();
        Task<City?> GetById(int id);
        Task<City> Create(CityDto cityDto);
        Task Update(UpdateCityDto updateCityDto);
        Task Delete(int id);
        Task Exist(int id);
    }

}
