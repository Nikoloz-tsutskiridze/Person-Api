using Person.Core.Domains;

namespace Person.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersWithBirthdayTodayAsync();
    }
}
