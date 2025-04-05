using BasePerson.Api.Domains;
using BasePerson.Api.Dtos;
using BasePerson.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Api.Data;

namespace BasePerson.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleRelativeController : Controller
    {
        private readonly PeopleRelativeRepository _peopleRelativeRepository;

        public PeopleRelativeController( PeopleRelativeRepository peopleRelativeRepository)
        {
            _peopleRelativeRepository = peopleRelativeRepository;
        }

        //GET: api/PeopleRelative
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneRelativePersonDto>>> GetPeopleRelative()
        {
            var relations = await _peopleRelativeRepository.GetAll();
            return Ok(relations);
        }

        //GET: api/PeopleRelative/Person/1
        [HttpGet("Person/{personId}")]
        public async Task<ActionResult<IEnumerable<PhoneRelativePersonDto>>> GetPeopleRelativeById(int personId)
        {
            var relations = await _peopleRelativeRepository.GetById(personId);
            if (!relations.Any()) return NotFound("No relations found for this person.");
            return Ok(relations);
        }

        //POST: api/PeopleRelative 
        [HttpPost]
        public async Task<ActionResult<int>> RelatePersonToPerson(PeopleRelativeDto peopleRelativeDto)
        {
            var relationId = await _peopleRelativeRepository.Create(peopleRelativeDto);
            return CreatedAtAction(nameof(GetPeopleRelativeById), new { personId = peopleRelativeDto.FirstPersonId }, relationId);
        }


        //DELETE: api/PeopleRelative/5
        [HttpDelete]
        public async Task<IActionResult> RemovePeopleRelative(int Id)
        {
            var result = await _peopleRelativeRepository.Delete(Id);
            return result ? NoContent() : NotFound($"Relation with ID {Id} not found.");
        }
    }
}
