using BasePerson.Api.Domains;
using BasePerson.Api.Dtos;
using BasePerson.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;

namespace BasePerson.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatePhoneController : ControllerBase
    {
        private readonly RelatePhoneRepository _relatePhoneRepository;

        public RelatePhoneController(RelatePhoneRepository relatePhoneRepository)
        {
            _relatePhoneRepository = relatePhoneRepository;
        }

        //GET: api/RelatePhone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneRelativePersonDto>>> GetPhoneRelations()
        {
            var relations = await _relatePhoneRepository.GetAll();
            return Ok(relations);
        }

        //GET: api/RelatePhone/Person/1
        [HttpGet("Person/{personId}")]
        public async Task<ActionResult<IEnumerable<PhoneRelativePersonDto>>> GetPhoneRelationsById(int personId)
        {
            var relations = await _relatePhoneRepository.GetById(personId);

            if (relations.Value == null || !relations.Value.Any())
                return NotFound("No phone relations found for this person.");

            return Ok(relations.Value);
        }

        //POST: api/RelatePhone
        [HttpPost]
        public async Task<ActionResult<int>> RelatePhoneToPerson(PhoneRelativePersonDto phoneRelativePersonDto)
        {
                var phoneRelativePerson = await _relatePhoneRepository.Create(phoneRelativePersonDto);
                return Ok(phoneRelativePerson.Id);
        }

        //DELETE: api/RelatePhone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePhoneRelation(int id)
        {
            var success = await _relatePhoneRepository.RemovePhoneRelation(id);
            if (!success)
                return NotFound($"The connection with id {id} doesn't exist.");

            return Ok();
        }
    }
}
