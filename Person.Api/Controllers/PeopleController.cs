using BasePerson.Core.Dtos;
using BasePerson.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BasePerson.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleService _peopleRepository;

        public PeopleController(PeopleService peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingCustomerDto>>> GetCustomer()
        {
            var people = await _peopleRepository.GetAll();
            return Ok(people);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var person = await _peopleRepository.GetById(id);
            return Ok(person);
        }


        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customerDto)
        {
            var createdCustomer = await _peopleRepository.Create(customerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
        }


        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(ExistingCustomerDto customerDto)
        {
            if (await _peopleRepository.Update(customerDto))
                return Ok();

            return NotFound();
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (await _peopleRepository.Delete(id))
                return Ok();

            return NotFound();
        }

        [HttpPost("ConnectPeople")]
        public async Task<int> ConnectPeople(PeopleRelativeDto peopleRelativeDto)
        {
            return await _peopleRepository.ConnectPeople(peopleRelativeDto);
        }

        [HttpDelete("DeleteConnectionPeople")]
        public async Task<bool> DisconnectPeople(int connectionId)
        {
            return await _peopleRepository.DisconnectPeople(connectionId);
        }

        [HttpPost("ConnectPhone")]
        public async Task<int> ConnectPhone(PhoneRelativePersonDto phoneRelativePersonDto)
        {
            return await _peopleRepository.ConnectPhone(phoneRelativePersonDto);
        }

        [HttpDelete("DeleteConnectionPhone")]
        public async Task<bool> DeleteConnectionPhone(int id)
        {
            return await _peopleRepository.DisconectPhone(id);
        }
    }
}
